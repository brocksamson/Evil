using System.Linq;
using Evil.Common;
using StructureMap;
using StructureMap.Graph;

namespace Evil.Infrastructure.Structuremap
{
    public abstract class Bootstrapper
    {
        public void Bootstrap()
        {
            ObjectFactory.Initialize(r =>
                r.Scan(assembly =>
                {
                    GetAssemblies(assembly);
                    if (assembly == null)
                        return;

                    assembly.AddAllTypesOf(typeof(IStartupTask));
                    assembly.LookForRegistries();
                }));

            ObjectFactory.GetAllInstances<IStartupTask>().ToList().ForEach(m => m.Execute());
            
        }

        protected abstract void GetAssemblies(IAssemblyScanner assembly);
    }
}
