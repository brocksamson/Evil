using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureMap;

namespace Evil.Infrastructure.EventWiring
{
    public class AutoEventGlue<TSource>
    {
        private readonly object _locker = new object();
        private readonly Dictionary<Type, IEnumerable<EventData>> _cachedEvents;
        private readonly IContainer _container;

        public AutoEventGlue(IContainer container)
        {
            _container = container;
            _cachedEvents = new Dictionary<Type, IEnumerable<EventData>>();
        }

        public void GenerateHandlers(TSource source)
        {
            //TODO: only works for events that do not follow MS best practices, i.e. doesn't work with object sender, EventArgs delegate signature
            //It is possible to use a DynamicMethod to wrap the IHandler.Handle method.  Since this will be dynamic IL attached to the real object
            //this should be very fast.
            var sourceType = typeof (TSource);
            var events = GetCachedEvents(sourceType);
            if (events == null)
            {
                events = GetEventsFromType(sourceType);
                SetCachedEvents(sourceType, events);
            }
            foreach (var eventData in events)
            {
                var handlers = _container.GetAllInstances(eventData.GenericHandlerType);
                foreach (var handler in handlers)
                {
                    var d = Delegate.CreateDelegate(eventData.Event.EventHandlerType, handler, eventData.HandlerMethod);
                    eventData.Event.AddEventHandler(source, d);
                }
            }
        }

        private void SetCachedEvents(Type sourceType, IEnumerable<EventData> events)
        {
            lock (_locker)
            {
                if(!_cachedEvents.ContainsKey(sourceType))
                    _cachedEvents.Add(sourceType, events);
            }
        }

        private IEnumerable<EventData> GetEventsFromType(Type sourceType)
        {
            return sourceType.GetEvents().Select(GetEventData).Where(eventData => eventData != null);
        }

        private IEnumerable<EventData> GetCachedEvents(Type sourceType)
        {
            return _cachedEvents.ContainsKey(sourceType) ? _cachedEvents[sourceType] : null;
        }

        private static EventData GetEventData(EventInfo eventInfo)
        {
            var methodInfo = eventInfo.EventHandlerType.GetMethods().First();
            var parameters = methodInfo.GetParameters();
            var parameterInfo = parameters.FirstOrDefault(m => typeof(IEventSource).IsAssignableFrom(m.ParameterType));
            if(parameters.Count() == 1 && parameterInfo != null)
            {
                var parameterType = parameterInfo.ParameterType;
                var genericHandlerType = typeof(IHandler<>).MakeGenericType(parameterType);
                var handlerMethod = genericHandlerType.GetMethod("Handle", new[] { parameterType });
                return new EventData
                           {
                               Event = eventInfo,
                               ParameterType = parameterType,
                               GenericHandlerType = genericHandlerType,
                               HandlerMethod = handlerMethod
                           };
            }
            return null;
        }

        private class EventData
        {
            public EventInfo Event { get; set; }
            public Type ParameterType { get; set; }
            public Type GenericHandlerType { get; set; }
            public MethodInfo HandlerMethod { get; set; }
        }
    }
}