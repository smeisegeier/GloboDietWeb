using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RecipesList : _ViewModelBase
    {
        public IEnumerable<Recipe> Recipes { get; set; }

        public RecipesList(IEnumerable<Recipe> recipes, NavigationBar navigationBar) : base(navigationBar)
        {
            Recipes = recipes;
        }
    }
}
