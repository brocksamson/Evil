using System;
using Evil.Agents;
using Evil.Engine;
using Evil.Lairs;

namespace Evil.Missions
{
    public class InfiltrationMission
    {
        private readonly IDice _dice;

        public InfiltrationMission(IDice dice)
        {
            _dice = dice;
        }

        public MissionDetails Begin(Agent agent, Lair target)
        {
            var details = new MissionDetails
                       {
                           MissionStart = DateTime.Now,
                           MissionDuration = new TimeSpan(1, 0, 0),
                           SuccessChance = .2M,
                           DiscoveryChance = .05M
                       };
            agent.BeginMission(details);
            return details;
        }

        public MissionOutcome Complete(Agent agent, Lair target)
        {
            var outcome = new MissionOutcome();
            agent.CompleteMission(outcome);
            return outcome;
        }
    }

    public class MissionOutcome
    {
    }
}