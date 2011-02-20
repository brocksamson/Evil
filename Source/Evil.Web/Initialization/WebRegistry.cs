using System.Web.Mvc;
using Evil.Common;
using StructureMap.Configuration.DSL;

namespace Evil.Web.Initialization
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(scanner =>
                     {
                         scanner.TheCallingAssembly();
                         scanner.WithDefaultConventions();
                         scanner.AddAllTypesOf<Controller>();
                         scanner.AddAllTypesOf<IStartupTask>();
                     });
            For<IDependencyResolver>().Use<StructureMapDependencyResolver>();
            For<IControllerActivator>().Use<StructureMapDependencyResolver>();
            For<IViewPageActivator>().Use<StructureMapDependencyResolver>();
            For<IControllerFactory>().Use(() => new DefaultControllerFactory());
            For<ModelMetadataProvider>().Use<DataAnnotationsModelMetadataProvider>();            
        }
    }
}