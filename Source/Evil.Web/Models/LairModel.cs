using System;
using System.Collections.Generic;

namespace Evil.Web.Models
{
    public class LairModel
    {
        public int Id { get; set; }
        public int EmptySections { get; set; }
        public DateTime? UpgradeFinished { get; set; }

        public bool CanUpgrade { get; set; }

        public IEnumerable<SectionType> AllowedSections { get; set; }
    }

    public enum SectionType
    {
        Game
    }
}