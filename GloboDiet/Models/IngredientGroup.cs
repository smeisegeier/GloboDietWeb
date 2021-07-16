using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Legacy.GloboDietDb;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GloboDiet.Models
{
    public class IngredientGroup : _ModelBase
    {
        public IngredientGroup() { }

        public static IEnumerable<IngredientGroup> GetSeedsFromLegacy()
        {
            var legacyList = GROUPS.GetLegacyObjects<GROUPS>();
            var newList = new List<IngredientGroup>();

            legacyList?.ToList().ForEach(srcitem =>
            {
                newList.Add(new IngredientGroup()
                {
                    Code = srcitem.GROUP,
                    Name = srcitem.NAME_SHORT,
                    Description = srcitem.NAME
                });
            });
            return newList.OrderBy(o => o.Code);
        }

        public static SelectList Dropdown { get; set; }
    }

}
