using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public static List<Location> GenerateDefaultValues()
        {
            return new List<Location>()
            {
                new Location() { City = "Heidelberg", Country = "DE" },
                new Location() { City = "Potsdam", Country = "DE" }
            };
        }
    }
}
