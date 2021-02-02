using GloboDiet.Legacy.GloboDietDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    // Lebensmittelkategorien?
    // Vorschläge Auswahl Mahlzeiten
    public class Essin : _ModelBase
    {
        public Essin() { }

        public static IEnumerable<Essin> GetSeedsFromLegacy()
        {
            var legacyList = PROBQUE.GetLegacyObjects<PROBQUE>();
            var newList = new List<Essin>();
            foreach (var item in legacyList)
            {
                newList.Add(new Essin()
                {
                    Code = item.PQ_CODE,
                    Name = item.PQ_TEXT
                });
            }
            return newList;
        }
    }
}
