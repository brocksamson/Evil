using System;
using System.Linq;
using Evil.Common;
using Evil.Events;
using StructureMap;

namespace Evil.Infrastructure.Tests.EventWiring
{
    public class AutoEventGlue<TSource>
    {
        private readonly IContainer _container;

        public AutoEventGlue(IContainer container)
        {
            _container = container;
        }

        public void GenerateHandlers(TSource source)
        {
            //TODO: only works for events that do not follow MS best practices, i.e. doesn't work with object sender, EventArgs delegate signature
            //It is possible to use a DynamicMethod to wrap the IHandler.Handle method.  Since this will be dynamic IL attached to the real object
            //this should be very fast.
            var sourceType = typeof (TSource);
            foreach (var eventInfo in sourceType.GetEvents())
            {
                var methodInfo = eventInfo.EventHandlerType.GetMethods().First();
                var parameters = methodInfo.GetParameters();
                var parameterInfo = parameters.FirstOrDefault(m => typeof(IEventSource).IsAssignableFrom(m.ParameterType));
                if(parameterInfo != null && parameters.Count() == 1)
                {
                    var genericHandlerType = typeof (IHandler<>).MakeGenericType(parameterInfo.ParameterType);
                    var handlerMethod = genericHandlerType.GetMethod("Handle", new[]{parameterInfo.ParameterType});
                    var handlers = _container.GetAllInstances(genericHandlerType);
                    foreach (var handler in handlers)
                    {
                        var d = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler, handlerMethod);
                        eventInfo.AddEventHandler(source, d);
                    }
                }
            }
        }
    }
}