using GloboDiet.Legacy.GloboDietDb;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string Group { get; set; }

        public Ingredient() { }

        public static IEnumerable<Ingredient> GetSeedsFromLegacy()
        {
            var legacyList = Foods.GetLegacyObjects<Foods>();
            var newList = new List<Ingredient>();

            legacyList?.ToList().ForEach(srcitem =>
            {
                newList.Add(new Ingredient()
                {
                    Code = srcitem.FOODNUM,
                    Group = srcitem.GROUP,
                    Name = srcitem.NAME
                });
            });
            return newList.OrderBy(x=>x.Name);
        }

        public static SelectList Dropdown { get; set; }
    }
}
