using System.Web.Mvc;
using System.Web.Routing;
using Evil.Common;
using Evil.Users;
using Evil.Web.Controllers;

namespace Evil.Web.Initialization
{
    public class RegisterRoutes : IStartupTask
    {
        #region IBootstrapperTask Members

        public void Execute()
        {
            RouteTable.Routes.Clear();
            Register(RouteTable.Routes);
        }

        #endregion

        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("start",
                            "start",
                            new {controller = "Game", action = "Start"});

            routes.MapRoute("missions",
                            "missions/{missionType}",
                            new
                                {
                                    controller = "Mission",
                                    action = "Index",
                                    currentPlayer = new Player(),
                                    missionType = MissionTypes.All.ToString()
                                });      
            routes.MapRoute("getMissions",
                            "GetMissions/{missionType}",
                            new
                                {
                                    controller = "Mission",
                                    action = "GetMissions",
                                    currentPlayer = new Player(),
                                    missionType = MissionTypes.All.ToString()
                                });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = ""} // Parameter defaults
                );
        }
    }
}