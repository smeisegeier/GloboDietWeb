using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Location : Base
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        [NotMapped]
        public new string Label { get => $"[{City} - {Country}]"; }

        public Location()
        {
        }

        public static IEnumerable<Location> GetSeededValues()
        {
            return new List<Location>()
            {
                new Location() { City = "Heidelberg", Country = "DE" },
                new Location() { City = "Potsdam", Country = "DE" }
            };
        }
    }
}
