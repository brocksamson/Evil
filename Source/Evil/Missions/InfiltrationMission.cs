using System;
using Evil.Agents;
using Evil.Engine;
using Evil.Lairs;

namespace Evil.Missions
{
    public class InfiltrationMission
    {
        public event MissionStartedHandler MissionStarted;
        public event MissionCompleteddHandler MissionCompleted;
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
            OnMissionStarted(details);
            return details;
        }

        private void OnMissionStarted(MissionDetails details)
        {
            if(MissionStarted != null)
                MissionStarted(this, details);
        }

        private void OnMissionCompleted(MissionOutcome outcome)
        {
            if (MissionCompleted != null)
                MissionCompleted(this, outcome);
        }

        public MissionOutcome Complete(Agent agent, Lair target)
        {
            var outcome = new MissionOutcome();
            agent.CompleteMission(outcome);
            OnMissionCompleted(outcome);
            return outcome;
        }

    }

    public delegate void MissionCompleteddHandler(object sender, MissionOutcome missionOutcome);
    public delegate void MissionStartedHandler(object sender, MissionDetails missionDetails);

    public class MissionOutcome
    {
    }
}