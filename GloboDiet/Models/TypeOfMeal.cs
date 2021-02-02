using GloboDiet.Legacy.GloboDietDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class TypeOfMeal : _ModelBase
    {
        public TypeOfMeal() { }

        public static IList<TypeOfMeal> GetSeedsFromLegacy()
        {
            var legacyList = FCO.GetLegacyObjects<FCO>();
            var newList = new List<TypeOfMeal>();
            foreach (var item in legacyList)
            {
                newList.Add(new TypeOfMeal()
                {
                    Name = item.FCM_SNAME,
                    Code = item.FCM_CODE,
                    Description = item.FCM_NAME
                });
            }
            return newList;
        }
    }
}
