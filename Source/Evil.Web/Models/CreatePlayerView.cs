using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Evil.Web.Models
{
    public class CreatePlayerView 
    {
        [Required]
        public string BaseName { get; set; }
        [Range(-90,90)]
        public double BaseLatitude { get; set; }
        [Range(-180, 180)]
        public double BaseLongitude { get; set; }
        [Required]
        public string Name { get; set; }
    }
}