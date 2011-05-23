using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Evil.Agents;
using Evil.Engine;
using Evil.Lairs;
using Evil.Missions;

namespace Evil.TestBed
{
    public class Program
    {
        static void Main(string[] args)
        {
            var agent = new Agent();
            var lair = new Lair();
            var dice = new Dice(m => new Random(DateTime.Now.Millisecond).Next(100));
            var mission = new InfiltrationMission(dice);


            //wire up begin event + the timed callback
            mission.AsObservable<MissionDetails>()
                .Subscribe(m =>
                               {
                                   Console.WriteLine("Mission Started");
                                   var missionTimer =
                                       Observable.Return(m).Delay(
                                           m.MissionStart.Add(m.MissionDuration), Scheduler.CurrentThread);
                                   missionTimer.Subscribe(_ => mission.Complete(agent));
                               },
                           onError => Console.WriteLine("Exception " + onError.Message));

            //wire up the mission complete event
            mission.AsObservable<MissionOutcome>()
                .Subscribe(outcome =>
                               {
                                   Console.WriteLine("Mission complete");
                                   mission.Begin(agent, lair);
                               });

            mission.Begin(agent, lair);
            Console.ReadLine();
        }

    }
}
