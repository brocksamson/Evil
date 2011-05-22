using System;
using Evil.Agents;
using Evil.Common;
using Evil.Events;
using Evil.Lairs;

namespace Evil.Missions
{
    public class MissionOutcome : IEventSource
    {
        public bool Success { get; set; }
        public Lair Target { get; set; }
        public Agent Agent { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool Discovered { get; set; }
    }
}