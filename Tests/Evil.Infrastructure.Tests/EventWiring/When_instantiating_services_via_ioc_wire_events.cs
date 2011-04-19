using System;
using System.Collections.Generic;
using Evil.Events;
using Evil.TestHelpers;
using MbUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace Evil.Infrastructure.Tests.EventWiring
{
    [TestFixture]
    public class When_instantiating_services_via_ioc_wire_events
    {
        private AutoEventGlue<FakeEventSource> _autoWire;
        private FakeEventSource _source;
        private IHandler<FakeEventHandlerArgs> _fakeEventHandler;
        private IContainer _container;

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
            _fakeEventHandler = MockRepository.GenerateMock<IHandler<FakeEventHandlerArgs>>();
            _container = MockRepository.GenerateMock<IContainer>();
            _container.Stub(m => m.GetAllInstances(typeof (IHandler<FakeEventHandlerArgs>)))
                .Return(new List<IHandler<FakeEventHandlerArgs>> {_fakeEventHandler});
            _autoWire = new AutoEventGlue<FakeEventSource>(_container);
        }

        [Test]
        public void Should_auto_wire_up_events()
        {
           _autoWire.GenerateHandlers(_source);

            const int input = 233;
            _source.Execute(input);
            var args = _fakeEventHandler.GetArgumentsForCallsMadeOn(m => m.Handle(null)).FirstOf<FakeEventHandlerArgs>();
            Assert.AreEqual(input, args.Input);
        }

        [Test]
        public void Will_only_auto_wire_same_signature()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Should_wire_up_fast()
        {
            throw new NotImplementedException();            
        }
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

    public class FakeEventHandlerArgs : IEventSource
    {
        public int Input { get; set; }
    }
}
