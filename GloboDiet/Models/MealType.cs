using GloboDiet.Legacy.GloboDietDb;
using GloboDiet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class MealType : _ModelBase
    {
        public MealType() { }

        public static IList<MealType> GetSeedsFromLegacy()
        {
            var legacyList = FCO.GetLegacyObjects<FCO>();
            var newList = new List<MealType>();
            foreach (var item in legacyList)
            {
                newList.Add(new MealType()
                {
                    Name = item.FCM_SNAME,
                    Code = item.FCM_CODE,
                    Description = item.FCM_NAME
                });
            }
            return newList;
        }

        public static implicit operator MealType(MealTypeCreateEdit viewModel) => new MealType
        {
            Id = viewModel.Id,
            Description = viewModel.Description,
            Code = viewModel.Code
        };
    }
}
