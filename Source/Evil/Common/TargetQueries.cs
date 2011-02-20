using System.Collections.ObjectModel;
using System.Linq;
using Evil.Users;

namespace Evil.Common
{
    public static class TargetQueries
    {
        public static IQueryable<Target> ValidTargetsFor(this IQueryable<Target> query, Player player)
        {
            //TODO: Not real!
            return (new Collection<Target>
                        {
                            new Target {Name = "Target 1", Position = new Position(40.751312, -73.9812)}
                        }).AsQueryable();
        }
    }
}