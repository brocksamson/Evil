using System.Web.Mvc;
using Evil.Common;

namespace Evil.Web.Initialization
{
    public class RegisterGlobalFilters : IStartupTask
    {
        public void Execute()
        {
            var filters = GlobalFilters.Filters;
            filters.Add(new HandleErrorAttribute());
        }
    }
}