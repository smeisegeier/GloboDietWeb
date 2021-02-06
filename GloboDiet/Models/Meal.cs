using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Meal : _ModelBase
    {
        [Range(0,23)]
        public int StartingHour { get; set; }

        public int TypeOfMealId { get; set; }
        public TypeOfMeal TypeOfMeal { get; set; }

        public int PlaceOfMealId { get; set; }
        public PlaceOfMeal PlaceOfMeal { get; set; }

        public ICollection<Essin> Essins{ get; set; }
    }
}
