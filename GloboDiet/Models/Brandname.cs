using GloboDiet.Legacy.GloboDietDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Brandname : _ModelBase 
    {
        public string Group { get; set; }
        public Brandname() { }

        public static IEnumerable<Brandname> GetSeedsFromLegacy()
        {
            var legacyList = Brandnam.GetLegacyObjects<Brandnam>();
            var newList = new List<Brandname>();
            foreach (var item in legacyList)
            {
                newList.Add(new Brandname()
                {
                    Name = item.NAME,
                    Group = item.GROUP
                });
            }
            return newList.OrderBy(x => x.Name);
        }
    }
}
