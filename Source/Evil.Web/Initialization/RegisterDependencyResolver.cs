using System.Web.Mvc;
using Evil.Common;

namespace Evil.Web.Initialization
{
    //TODO: Should this be up in the infrastructure?
    public class RegisterDependencyResolver : IStartupTask
    {
        private readonly IDependencyResolver _dependencyResolver;

        public RegisterDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Execute()
        {
            DependencyResolver.SetResolver(_dependencyResolver);
        }
    }
}
