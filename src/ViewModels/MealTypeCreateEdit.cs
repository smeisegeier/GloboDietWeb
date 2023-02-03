using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class MealTypeCreateEdit : _ViewModelBase
    {
        public static implicit operator MealTypeCreateEdit(MealType model)
        {
            var viewModel = new MealTypeCreateEdit
            {
                Id = model.Id,
                Description = model.Description,
                Code = model.Code
            };
            viewModel.Init();
            return viewModel;
        }
    }
}
