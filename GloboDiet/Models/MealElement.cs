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

        public int IngredientId { get; set; }
        //public virtual Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
}
