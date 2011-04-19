using System;
using System.Collections.Generic;
using Evil.TestHelpers;
using MbUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace Evil.Infrastructure.Tests.EventWiring
{
    [TestFixture]
    public class When_instantiating_services_via_Ioc_wire_events
    {
        private AutoEventGlue<FakeEventSource> _autoWire;
        private FakeEventSource _source;
        private IHandler<EventArgs> _fakeEventHandler;
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
            _fakeEventHandler = MockRepository.GenerateMock<IHandler<EventArgs>>();
            _container = MockRepository.GenerateMock<IContainer>();
            _autoWire = new AutoEventGlue<FakeEventSource>(_container);
        }

        [Test]
        public void Should_auto_wire_up_events()
        {
           _autoWire.GenerateHandlers(_source);

            const int input = 233;
            _source.Execute(input);
            var args = _fakeEventHandler.GetArgumentsForCallsMadeOn(m => m.Execute(null)).First<FakeEventHandlerArgs>();
            Assert.AreEqual(input, args.Input);
        }
    }

    public class AutoEventGlue<TSource>
    {
        private readonly IContainer _container;

        public AutoEventGlue(IContainer container)
        {
            _container = container;
        }

        public void GenerateHandlers(TSource source)
        {
            var sourceType = typeof (TSource);

            foreach (var eventInfo in sourceType.GetEvents())
            {

                var eventSignature = eventInfo.GetAddMethod();
                foreach (var parameterInfo in eventSignature.GetParameters())
                {
                    Console.WriteLine(parameterInfo.ParameterType.Name);
                }
            }
        }
    }

    public interface IHandler<in T> where T : EventArgs
    {
        void Execute(T args);
    }

    public class FakeEventSource
    {
        public delegate void FakeEventHandler(object sender, FakeEventHandlerArgs args);

        public event FakeEventHandler FakeEvent;

        public void Execute(int input)
        {
            if(FakeEvent != null)
                FakeEvent(this, new FakeEventHandlerArgs{Input = input});
        }
    }

    public class FakeEventHandlerArgs : EventArgs
    {
        public int Input { get; set; }
    }
}
