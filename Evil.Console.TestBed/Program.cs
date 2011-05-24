using System;
using System.Reactive.Linq;
using Evil.Agents;
using Evil.Engine;
using Evil.Infrastructure.Structuremap;
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


            var timer = new MissionTimer(new LocatorImp());
            timer.Subscribe(mission);
           
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
