using System.Collections.Generic;

namespace Evil.Web.Models
{
    public class BaseView
    {
        public int Id { get; set; }
        public IEnumerable<SectionView> Sections { get; set; }
    }

    public class SectionView
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public SectionType Type { get; set; }

        public bool CanUpgrade { get; set; }

        public IEnumerable<JobView> Jobs { get; set; }
    }

    public class JobView
    {
        public bool IsVacant { get; set; }
        public AgentView AssignedAgent { get; set; }
    }

    public enum SectionType
    {
        Empty,
        Bar
    }
}