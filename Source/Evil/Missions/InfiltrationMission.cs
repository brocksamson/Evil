using System;
using System.Linq;
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

        public void Begin(Agent agent, Lair target)
        {
            var details = new MissionDetails
                       {
                           MissionStart = DateTime.Now,
                           MissionDuration = new TimeSpan(1, 0, 0),
                           SuccessChance = CalculateSuccessChance(agent, target),
                           DiscoveryChance = CalculateDiscoveryChance(agent, target),
                           Target = target
                       };
            agent.BeginMission(details);
            OnMissionStarted(details);
        }


        public void Complete(Agent agent)
        {
            if (agent.CurrentMission == null) throw new ArgumentException("Could not complete a mission, agent is currently idle.");
            var outcome = new MissionOutcome
                              {
                                  Success = _dice.RollPercentage(agent.CurrentMission.SuccessChance),
                                  Discovered = _dice.RollPercentage(agent.CurrentMission.DiscoveryChance)
                              };
            agent.CompleteMission(outcome);
            OnMissionCompleted(outcome);
        }

        private static decimal CalculateDiscoveryChance(Agent agent, Lair target)
        {
            var tries = agent.MissionHistory.Count(query => query.Target == target);
            return .05M + (.05M * tries);
        }

        private static decimal CalculateSuccessChance(Agent agent, Lair target)
        {
            var tries = agent.MissionHistory.Count(query => query.Target == target);
            return .20M + (.05M*tries);
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
    }
}