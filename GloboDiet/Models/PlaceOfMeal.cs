using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Services;

namespace GloboDiet.Models
{
    [Table(nameof(GloboDietDbContext.PlacesOfMeal))]
    public class PlaceOfMeal : Base
    {
        public string Place { get; set; }

        public PlaceOfMeal()
        {
        }

        public static List<PlaceOfMeal> GenerateDefaultValues()
        {
            return new List<PlaceOfMeal>()
                {
                    new PlaceOfMeal() { Place = "vor Ort"},
                    new PlaceOfMeal() { Place = "im Bus"}
                };
        }

    }
}
