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

        //private MealElement _mealElement { get; set; }

        //public MealElementCreateEdit(MealElement mealElement, NavigationBar navigationBar) : base(navigationBar, Globals.ProcessMilestone._3_MEALS)
        //{
        //    _mealElement = mealElement;
        //}

        public new int Id { get; set; } 

        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }

        public new string Name { get; set; }

        public int Quantity { get; set; }

        public static implicit operator MealElementCreateEdit(MealElement model) => new MealElementCreateEdit
        {
            Id = model.Id,
            MealId = model.MealId,
            Name = model.Name,
            Quantity = model.Quantity
        };


    }
}
