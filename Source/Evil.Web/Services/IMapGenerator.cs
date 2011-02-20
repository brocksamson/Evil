using System.Collections.Generic;
using Evil.Common;
using Evil.Web.Models;

namespace Evil.Web.Services
{
    public interface IMapGenerator
    {
        GoogleMap CreateStartingMap(Area city);
        GoogleMap GenerateTargetMap(IEnumerable<Target> mission);
    }
}