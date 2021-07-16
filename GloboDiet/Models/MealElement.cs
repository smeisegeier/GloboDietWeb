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
        [ForeignKey(nameof(GloboDiet.Models.Ingredient))]
        public int? IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        // Group (down)
        [ForeignKey(nameof(GloboDiet.Models.IngredientGroup))]
        public int? IngredientGroupId { get; set; }
        public virtual IngredientGroup IngredientGroup { get; set; }

        // Brandname (down)
        [ForeignKey(nameof(GloboDiet.Models.Brandname))]
        public int? BrandnameId { get; set; }
        public virtual Brandname Brandname { get; set; }

        public int Quantity { get; set; }

        public bool IsCachedOnly { get; set; } = true;

        //public string ImagePath { get; set; }

        // FoodImage (down)
        [ForeignKey(nameof(GloboDiet.Models.FoodImage))]
        public int? FoodImageId { get; set; }
        public virtual FoodImage FoodImage { get; set; }


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
                FoodImageId = viewModel.FoodImageId,
                IsCachedOnly = viewModel.IsCachedOnly,
                //ImagePath = viewModel.ImagePath
            };
            return mealElement;
        }

        public static List<MealElement> GetSeedsFromMockup()
        {
            return new List<MealElement>()
                {
                    new MealElement()
                    {
                        MealId=1,
                        IngredientId=124,
                        IngredientGroupId=2,
                        BrandnameId=2,
                        Quantity=1,
                        Id=1,
                        //ImagePath="/images/apple1.png",
                        FoodImageId = 1,
                        //FoodImage = FoodImage.GetSeedsFromLegacy()[0],
                        Name="xde",
                        Description="-- desc --",
                        Code="07",
                        CreatedAt=DateTime.Now
                    }
                };
        }
    }
}
