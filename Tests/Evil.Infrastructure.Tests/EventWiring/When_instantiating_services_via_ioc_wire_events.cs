using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Evil.Infrastructure.EventWiring;
using Evil.TestHelpers;
using MbUnit.Framework;
using Rhino.Mocks;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Query;

namespace Evil.Infrastructure.Tests.EventWiring
{
    [TestFixture]
    public class When_instantiating_services_via_ioc_wire_events
    {
        private AutoEventGlue<FakeEventSource> _autoWire;
        private FakeEventSource _source;
        private FakeHandler _fakeEventHandler;
        private IContainer _container;
        private List<FakeHandler> _handlers;

        [Test]
        public void Should_wire_up_events()
        {
            var source = new FakeEventSource();
            var executed = false;
            source.FakeEvent += delegate { executed = true; };


            source.Execute(111);
            Assert.IsTrue(executed, "Fake Event not wired up");
        }

        [SetUp]
        public void Arrange()
        {
            _source = new FakeEventSource();
            _fakeEventHandler = new FakeHandler();
            _handlers = new List<FakeHandler> {_fakeEventHandler};
            while(_handlers.Count < 5)
            {
                _handlers.Add(new FakeHandler());
            }
            _container = new FakeContainer(_handlers.ToArray());
            _autoWire = new AutoEventGlue<FakeEventSource>(_container);
        }

        [Test]
        public void Should_auto_wire_up_events()
        {
           _autoWire.GenerateHandlers(_source);

            const int input = 233;
            _source.Execute(input);
            Assert.AreEqual(input, _fakeEventHandler.Value);
        }

        [Test, Ignore("Not implemented")]
        public void Will_only_auto_wire_same_signature()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Should_wire_up_fast()
        {
            var watch = Stopwatch.StartNew();
            var instanceCount = 100000;
            for (int i = 0; i < instanceCount; i++)
            {
                _source = new FakeEventSource();
                foreach (var fakeHandler in _handlers)
                {
                    _source.FakeEvent += fakeHandler.Handle;
                    _source.Execute(i);                    
                }
                Assert.AreEqual(i, _fakeEventHandler.Value);
            }
            watch.Stop();
            Console.WriteLine("took {0} milliseconds to wire up {1} instances natively", watch.ElapsedMilliseconds, instanceCount * _handlers.Count);
            watch = Stopwatch.StartNew();
            for (int i = 0; i < instanceCount; i++)
            {
                _source = new FakeEventSource();
                _autoWire.GenerateHandlers(_source);
                _source.Execute(i);
                Assert.AreEqual(i, _fakeEventHandler.Value);
            }
            watch.Stop();
            Console.WriteLine("took {0} milliseconds to wire up {1} instances with auto wireup", watch.ElapsedMilliseconds, instanceCount * _handlers.Count);

            /* initial no-caching numbers
             * *** ConsoleOutput ***
            took 281 milliseconds to wire up 100000 instances natively
            took 6966 milliseconds to wire up 100000 instances with auto wireup
             */
        }
    }

    public class FakeContainer : IContainer
    {
        private List<FakeHandler> _handlers;
        public FakeContainer(params FakeHandler[] fakeEventHandlers)
        {
            _handlers = new List<FakeHandler>(fakeEventHandlers);
        }

        public void Dispose()
        {
        }

        public object GetInstance(Type pluginType, string instanceKey)
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type pluginType)
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type pluginType, Instance instance)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>(string instanceKey)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>()
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>(Instance instance)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAllInstances<T>()
        {
            throw new NotImplementedException();
        }

        public IList GetAllInstances(Type pluginType)
        {
            return _handlers;
        }

        public object TryGetInstance(Type pluginType, string instanceKey)
        {
            throw new NotImplementedException();
        }

        public object TryGetInstance(Type pluginType)
        {
            throw new NotImplementedException();
        }

        public T TryGetInstance<T>()
        {
            throw new NotImplementedException();
        }

        public T TryGetInstance<T>(string instanceKey)
        {
            throw new NotImplementedException();
        }

        public T FillDependencies<T>()
        {
            throw new NotImplementedException();
        }

        public object FillDependencies(Type type)
        {
            throw new NotImplementedException();
        }

        public void Configure(Action<ConfigurationExpression> configure)
        {
            throw new NotImplementedException();
        }

        public void Inject<PLUGINTYPE>(PLUGINTYPE instance)
        {
            throw new NotImplementedException();
        }

        public void Inject<PLUGINTYPE>(string name, PLUGINTYPE value)
        {
            throw new NotImplementedException();
        }

        public void Inject(Type pluginType, object @object)
        {
            throw new NotImplementedException();
        }

        public void SetDefaultsToProfile(string profile)
        {
            throw new NotImplementedException();
        }

        public string WhatDoIHave()
        {
            throw new NotImplementedException();
        }

        public void AssertConfigurationIsValid()
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAllInstances<T>(ExplicitArguments args)
        {
            throw new NotImplementedException();
        }

        public IList GetAllInstances(Type type, ExplicitArguments args)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>(ExplicitArguments args)
        {
            throw new NotImplementedException();
        }

        public ExplicitArgsExpression With<T>(T arg)
        {
            throw new NotImplementedException();
        }

        public IExplicitProperty With(string argName)
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type pluginType, ExplicitArguments args)
        {
            throw new NotImplementedException();
        }

        public void EjectAllInstancesOf<T>()
        {
            throw new NotImplementedException();
        }

        public void BuildUp(object target)
        {
            throw new NotImplementedException();
        }

        public void SetDefault(Type pluginType, Instance instance)
        {
            throw new NotImplementedException();
        }

        public Container.OpenGenericTypeExpression ForGenericType(Type templateType)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>(ExplicitArguments args, string name)
        {
            throw new NotImplementedException();
        }

        public ExplicitArgsExpression With(Type pluginType, object arg)
        {
            throw new NotImplementedException();
        }

        public CloseGenericTypeExpression ForObject(object subject)
        {
            throw new NotImplementedException();
        }

        public IContainer GetNestedContainer()
        {
            throw new NotImplementedException();
        }

        public IContainer GetNestedContainer(string profileName)
        {
            throw new NotImplementedException();
        }

        public IModel Model
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class FakeHandler
    {
        public void Handle(FakeEventHandlerArgs args)
        {
            Value = args.Input;
        }

        public int Value { get; private set; }
    }

    public class FakeEventSource
    {
        public delegate void FakeEventHandler(FakeEventHandlerArgs args);

        public event FakeEventHandler FakeEvent;

        public void Execute(int input)
        {
            if(FakeEvent != null)
                FakeEvent(new FakeEventHandlerArgs{Input = input});
        }
    }

    public class FakeEventHandlerArgs
    {
        public int Input { get; set; }
    }
}
