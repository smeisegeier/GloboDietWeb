using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Location : IEntity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public static IEnumerable<Location> GenerateDefaultValues()
        {
            return new List<Location>()
            {
                new Location() { City = "Heidelberg", Country = "DE" },
                new Location() { City = "Potsdam", Country = "DE" }
            };
        }
    }
}
