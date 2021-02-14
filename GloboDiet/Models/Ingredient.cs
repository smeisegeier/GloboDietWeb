using GloboDiet.Legacy.GloboDietDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    // Lebensmittelkategorien?
    // Vorschläge Auswahl Mahlzeiten
    public class Ingredient : _ModelBase
    {
        public Ingredient() { }

        public static IEnumerable<Ingredient> GetSeedsFromLegacy()
        {
            var legacyList = Foods.GetLegacyObjects<Foods>();
            var newList = new List<Ingredient>();
            foreach (var item in legacyList)
            {
                newList.Add(new Ingredient()
                {
                    Code = item.FOODNUM,
                    Name = item.NAME
                });
            }
            return newList;
        }
    }
}
