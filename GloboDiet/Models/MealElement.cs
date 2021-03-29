using GloboDiet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class MealElement : _ModelBase
    {

        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }

        [ForeignKey(nameof(Ingredient))]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }


        public MealElement() { }
        public MealElement(int mealId)
        {
            MealId = mealId;
        }

        public static implicit operator MealElement(MealElementCreateEdit viewModel)
        {
            var mealElement = new MealElement
            {
                Id = viewModel.Id,
                MealId = viewModel.MealId,
                Name = viewModel.Name,
                Quantity = viewModel.Quantity,
                IngredientId = viewModel.IngredientId
            };
            return mealElement;
        }

    }
}
