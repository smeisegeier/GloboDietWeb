using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Legacy.GloboDietDb;
using GloboDiet.Services;
using GloboDiet.Extensions;

namespace GloboDiet.Models
{
    [Table(nameof(GloboDietDbContext.PlacesOfMeal))]
    public class PlaceOfMeal : _ModelBase
    {
        public PlaceOfMeal()
        {
        }

        public static List<PlaceOfMeal> GetSeedsFromMockup()
        {
            return new List<PlaceOfMeal>()
                {
                    new PlaceOfMeal() { Name = "vor Ort"},
                    new PlaceOfMeal() { Name = "im Bus"}
                };
        }

        public static IEnumerable<PlaceOfMeal> GetSeedsFromLegacy()
        {
            var legacyList = POC.GetLegacyObjects<POC>();
            var newList = new List<PlaceOfMeal>();
            foreach (var item in legacyList)
            {
                newList.Add(new PlaceOfMeal()
                {
                    Name = item.POC_NAME,
                    Code = item.POC_CODE
                }) ;
            }
            return newList;
        }

    }
}
