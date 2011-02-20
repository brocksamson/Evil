using System.Web.UI.WebControls;
using Evil.Web.Binders;

namespace Evil.Web.Models
{
    public class AgentView
    {
        public int Id { get; set; }
        [DefaultSort(SortDirection.Ascending)]
        public string Name { get; set; }
        public int Level { get; set; }
        public AgentClass AgentClass { get; set; }
    }

    public enum AgentClass
    {
        Robber,
        Spy,
        Thug
    }
}