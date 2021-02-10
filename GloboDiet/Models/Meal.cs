using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Meal : _ModelBase
    {
        [Range(0,23)]
        public int StartingHour { get; set; }

        public int MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public int MealPlaceId { get; set; }
        public virtual MealPlace MealPlace { get; set; }

        [ForeignKey(nameof(Interview))]
        public int InterviewId { get; set; }

        public virtual ICollection<Essin> Essins{ get; set; }
    }
}
