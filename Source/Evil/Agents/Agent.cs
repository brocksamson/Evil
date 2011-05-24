using System;
using System.Collections.Generic;
using Evil.Common;
using Evil.Missions;

namespace Evil.Agents
{
    public class Agent : Entity
    {
        public virtual string Name { get; set; }
        public virtual MissionBrief CurrentMission { get; set; }
        public virtual List<MissionOutcome> MissionHistory { get; set; }

        public Agent()
        {
            MissionHistory = new List<MissionOutcome>();
        }

        public virtual void BeginMission(MissionBrief mission)
        {
            if(CurrentMission != null) throw new ArgumentException("Could not begin a mission, agent already has a mission.");
            mission.Agent = this;
            CurrentMission = mission;
        }

        public virtual void CompleteMission(MissionOutcome outcome)
        {
            if(CurrentMission == null) throw new ArgumentException("Could not complete a mission, agent is currently idle.");
            if(CurrentMission.MissionStart.Add(CurrentMission.MissionDuration) > DateTime.Now) throw new ArgumentNullException("Could not complete mission, duration not completed");
            outcome.Agent = this;
            outcome.Target = CurrentMission.Target;
            CurrentMission = null;
            MissionHistory.Add(outcome);
        }
    }
}