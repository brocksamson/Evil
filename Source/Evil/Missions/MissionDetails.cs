﻿using System;
using Evil.Common;

namespace Evil.Missions
{

    //DEBT: Is this a value object?  I think it is...
    public class MissionDetails : Entity
    {
        public DateTime MissionStart { get; set; }
        public TimeSpan MissionDuration { get; set; }
        public decimal SuccessChance { get; set; }
        public decimal DiscoveryChance { get; set; }
    }
}