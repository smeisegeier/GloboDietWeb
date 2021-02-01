using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RecipeCreateEdit : _ViewModelBase
    {
        private Recipe _recipe;

        public RecipeCreateEdit(Recipe recipe, NavigationBar navigationBar) : base(navigationBar)
        {
            _recipe = recipe;
        }

        public int Id { get => _recipe.Id; }
        public string Name { get => _recipe.Name; set { _recipe.Name = value; } }
        public string Description { get => _recipe.Description; set { _recipe.Description = value; } }
    }
}
