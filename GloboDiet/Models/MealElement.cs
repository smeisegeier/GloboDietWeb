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
        // Meal (up)
        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }

        // Ingredient (down)
        [ForeignKey(nameof(Ingredient))]
        public int? IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        // Group
        [ForeignKey(nameof(IngredientGroup))]
        public int? IngredientGroupId { get; set; }
        public virtual IngredientGroup IngredientGroup { get; set; }

        // Brandname
        [ForeignKey(nameof(Brandname))]
        public int? BrandnameId { get; set; }
        public virtual Brandname Brandname { get; set; }

        public int Quantity { get; set; }

        public bool IsCachedOnly { get; set; } = true;

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
                IngredientId = viewModel.IngredientId,
                IngredientGroupId = viewModel.IngredientGroupId,
                BrandnameId = viewModel.BrandnameId,
                IsCachedOnly = viewModel.IsCachedOnly
            };
            return mealElement;
        }

    }
}
