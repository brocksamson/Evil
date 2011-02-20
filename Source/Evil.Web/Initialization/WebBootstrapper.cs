using System;
using Evil.Common;
using Evil.Infrastructure.Structuremap;
using StructureMap.Graph;

namespace Evil.Web.Initialization
{
    public class WebBootstrapper : Bootstrapper
    {
        protected override void GetAssemblies(IAssemblyScanner assembly)
        {
            assembly.TheCallingAssembly();
            assembly.AssemblyContainingType<Bootstrapper>();
            assembly.AssemblyContainingType<Entity>();
        }
    }
}