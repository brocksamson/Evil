using System;
using Evil.Agents;
using Evil.Common;
using Evil.Lairs;

namespace Evil.Missions
{

    //DEBT: Is this a value object?  I think it is...
    public class MissionBrief : Entity
    {
        public virtual DateTimeOffset MissionStart { get; set; }
        public virtual TimeSpan MissionDuration { get; set; }
        public virtual decimal SuccessChance { get; set; }
        public virtual decimal DiscoveryChance { get; set; }
        public virtual Lair Target { get; set; }
        public virtual Agent Agent { get; set; }

        public virtual DateTimeOffset GetEstimatedCompletion()
        {
            return MissionStart.Add(MissionDuration);
        }
    }
}