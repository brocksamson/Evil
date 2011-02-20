using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Evil.Web.Initialization
{
    public class StructureMapDependencyResolver : IDependencyResolver, IControllerActivator, IViewPageActivator
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController)_container.GetInstance(controllerType);
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            return _container.GetInstance(type);
        }
    }
}
