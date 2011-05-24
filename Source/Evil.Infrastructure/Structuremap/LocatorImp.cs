using Evil.Common;
using StructureMap;

namespace Evil.Infrastructure.Structuremap
{
    public class LocatorImp : ILocator
    {
        public T GetInstance<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}