using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class MealElementCreateEdit : _ViewModelBase
    {
        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }

        [ForeignKey(nameof(Ingredient))]
        [Display(Name = "Ingredient")]
        public int IngredientId { get; set; }
        public string IngredientLabel { get; set; }


        [ForeignKey(nameof(IngredientGroup))]
        [Display(Name = "IngredientGroup")]
        public int IngredientGroupId { get; set; }
        public string IngredientGroupLabel { get; set; }


        public int Quantity { get; set; }

        public static implicit operator MealElementCreateEdit(MealElement model)
        {
            var mealElementCreateEdit = new MealElementCreateEdit
            {
                Id = model.Id,
                MealId = model.MealId,
                Name = model.Name,
                Quantity = model.Quantity,
                IngredientId = model.IngredientId,
                IngredientLabel = model.Ingredient is null ? "empty" : model.Ingredient.Label,
                IngredientGroupId = model.IngredientGroupId,
                IngredientGroupLabel = model.IngredientGroup is null ? "empty" : model.IngredientGroup.Label
            };
            return mealElementCreateEdit;
        }
    }
}
