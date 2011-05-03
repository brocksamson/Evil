using System;
using Evil.Agents;
using Evil.Engine;
using Evil.Lairs;

namespace Evil.Missions
{
    public class InfiltrationMission
    {
        public delegate void MissionCompleteddHandler(MissionOutcome missionOutcome);
        public delegate void MissionStartedHandler(MissionDetails missionDetails);

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
                MissionStarted(details);
        }

        private void OnMissionCompleted(MissionOutcome outcome)
        {
            if (MissionCompleted != null)
                MissionCompleted(outcome);
        }

        public MissionOutcome Complete(Agent agent, Lair target)
        {
            var outcome = new MissionOutcome();
            agent.CompleteMission(outcome);
            OnMissionCompleted(outcome);
            return outcome;
        }

    }
}