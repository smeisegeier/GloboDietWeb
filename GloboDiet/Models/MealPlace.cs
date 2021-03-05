﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Legacy.GloboDietDb;
using GloboDiet.Services;
using GloboDiet.Extensions;
using GloboDiet.ViewModels;

namespace GloboDiet.Models
{
    public class MealPlace : _ModelBase
    {
        public MealPlace()
        {
        }

        public static List<MealPlace> GetSeedsFromMockup()
        {
            return new List<MealPlace>()
                {
                    new MealPlace() { Name = "vor Ort"},
                    new MealPlace() { Name = "im Bus"}
                };
        }

        public static IList<MealPlace> GetSeedsFromLegacy()
        {
            var legacyList = POC.GetLegacyObjects<POC>();
            var newList = new List<MealPlace>();
            foreach (var item in legacyList)
            {
                newList.Add(new MealPlace()
                {
                    Name = item.POC_NAME,
                    Code = item.POC_CODE
                }) ;
            }
            return newList;
        }

        public static implicit operator MealPlace(MealPlaceCreateEdit viewModel) => new MealPlace
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Code = viewModel.Code
        };

    }
}
