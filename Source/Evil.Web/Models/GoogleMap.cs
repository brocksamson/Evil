using System.Collections.Generic;
using Evil.Common;

namespace Evil.Web.Models
{
    public class GoogleMap
    {
        public int Zoom { get; set; }
        public IEnumerable<GoogleLocation> Locations { get; set; }
        public Position StartingPosition { get; set; }
    }
}