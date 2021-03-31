using GloboDiet.Services;
using GloboDiet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Location : _ModelBase
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        // needed for dropdown
        [NotMapped]
        public new string Label { get => ToString(); }

        public Location()
        {
        }

        public static IEnumerable<Location> GetSeedsFromMockup()
        {
            return new List<Location>()
            {
                new Location() { City = "Heidelberg", Country = "DE" },
                new Location() { City = "Potsdam", Country = "DE" }
            };
        }

        public override string ToString() => $"[{City} - {Country}]";

        public static implicit operator Location(LocationCreateEdit viewModel)
        {
            var model = new Location
            {
                Id = viewModel.Id,
                City = viewModel.City,
                Country = viewModel.Country
            };
            return model;
        }

    }
}
