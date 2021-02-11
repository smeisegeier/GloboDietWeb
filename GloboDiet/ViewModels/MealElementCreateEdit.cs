using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class MealElementCreateEdit : _ViewModelBase
    {

        private MealElement _mealElement { get; set; }

        public MealElementCreateEdit(MealElement mealElement, NavigationBar navigationBar)
        {
            _mealElement = mealElement;
            NavigationBar = navigationBar;
        }

        public int Id => _mealElement.Id;

        [ForeignKey(nameof(Meal))]
        public int MealId => _mealElement.MealId;

        public string Name
        {
            get => _mealElement.Name;
            set { _mealElement.Name = value; }
        }

        public int Quantity
        {
            get => _mealElement.Quantity;
            set { _mealElement.Quantity = value; }
        }
    }
}
