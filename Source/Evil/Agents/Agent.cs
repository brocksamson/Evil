using System;
using System.Collections.Generic;
using Evil.Common;
using Evil.Missions;

namespace Evil.Agents
{
    public class Agent : Entity
    {
        public virtual string Name { get; set; }

        public MissionDetails CurrentMission { get; set; }

        public List<MissionOutcome> MissionHistory { get; set; }

        public Agent()
        {
            MissionHistory = new List<MissionOutcome>();
        }

        public void BeginMission(MissionDetails mission)
        {
            if(CurrentMission != null) throw new ArgumentException("Could not begin a mission, agent already has a mission.");
            CurrentMission = mission;
        }

        public void CompleteMission(MissionOutcome outcome)
        {
            if(CurrentMission == null) throw new ArgumentException("Could not complete a mission, agent is currently idle.");
            if(CurrentMission.MissionStart.Add(CurrentMission.MissionDuration) > DateTime.Now) throw new ArgumentNullException("Could not complete mission, duration not completed");
            outcome.Agent = this;
            outcome.Target = CurrentMission.Target;
            MissionHistory.Add(outcome);
        }
    }
}