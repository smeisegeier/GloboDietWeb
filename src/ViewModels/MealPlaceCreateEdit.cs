using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class MealPlaceCreateEdit : _ViewModelBase
    {
        public static implicit operator MealPlaceCreateEdit(MealPlace model)
        {
            var viewModel = new MealPlaceCreateEdit
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code
            };
            return viewModel;
        }
    }
}
