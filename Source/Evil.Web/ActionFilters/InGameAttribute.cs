using System.Web.Mvc;
using Evil.Web.Services;

namespace Evil.Web.ActionFilters
{
    public class InGameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DependencyResolver.Current.GetService<InGameFilter>().OnActionExecuting(filterContext);
        }

    }

    public class InGameFilter
    {
        private const string GameStartUrl = "~/Game/Start";
        private readonly IUserProvider _userProvider;

        public InGameFilter(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_userProvider.CurrentPlayer() == null)
                filterContext.Result = new RedirectResult(GameStartUrl);
            
        }

    }

}