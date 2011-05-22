using System;
using System.Reactive.Linq;
using System.Threading;
using MbUnit.Framework;

namespace Evil.Infrastructure.Tests.EventWiring
{
    [TestFixture]
    public class When_using_rx_for_events
    {
        [Test]
        public void blah()
        {
            Observable.FromEvent()
            var oneNumberEveryFiveSeconds = Observable.Interval(TimeSpan.FromSeconds(5));

            // Instant echo
            oneNumberEveryFiveSeconds.Subscribe(num =>
            {
                Console.WriteLine(num);
            });

            // One second delay
            oneNumberEveryFiveSeconds.Delay(TimeSpan.FromSeconds(1)).Subscribe(num =>
            {
                Console.WriteLine("...{0}...", num);
            });

            // Two second delay
            oneNumberEveryFiveSeconds.Delay(TimeSpan.FromSeconds(2)).Subscribe(num =>
            {
                Console.WriteLine("......{0}......", num);
            });
            Thread.Sleep(20000);
        }
    }
}
