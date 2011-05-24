using System;
using Evil.Agents;
using Evil.Common;
using Evil.Lairs;

namespace Evil.Missions
{
    public class MissionOutcome : Entity
    {
        public virtual bool Success { get; set; }
        public virtual Lair Target { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual DateTime CompletionDate { get; set; }
        public virtual bool Discovered { get; set; }
    }
}