using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Evil.Web.ActionFilters;
using Evil.Web.Binders;
using Evil.Web.Models;

namespace Evil.Web.Controllers
{
    public class AgentController : Controller
    {        
        public ActionResult Index(Sorter<AgentView> sortBy)
        {
            var agentList = GetFakeAgents();
            return View(sortBy.Sort(agentList));
        }

        private static IEnumerable<AgentView> GetFakeAgents()
        {
            return new Collection<AgentView>
                       {
                           new AgentView {Id = 0, AgentClass = AgentClass.Robber, Level = 4, Name = "First Robber"},
                           new AgentView {Id = 1, AgentClass = AgentClass.Thug, Level = 1, Name = "First Thug"},
                           new AgentView {Id = 2, AgentClass = AgentClass.Spy, Level = 14, Name = "Spy 1"},
                           new AgentView {Id = 3, AgentClass = AgentClass.Thug, Level = 44, Name = "Another Thug"},
                           new AgentView {Id = 4, AgentClass = AgentClass.Robber, Level = 5, Name = "Robber.The.Second"},
                           new AgentView {Id = 5, AgentClass = AgentClass.Spy, Level = 2, Name = "Spy Unseen"},
                           new AgentView {Id = 6, AgentClass = AgentClass.Robber, Level = 7, Name = "McRobbin"}
                       };
        }
    }
}
