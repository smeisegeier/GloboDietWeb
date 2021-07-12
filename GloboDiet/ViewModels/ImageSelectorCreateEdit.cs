using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class ImageSelectorCreateEdit : _ViewModelBase
    {
        public List<FoodImage> ListOfFoodImages { get; set; }

        public int MealElementId { get; set; } 

        public ImageSelectorCreateEdit Init(
            List<FoodImage> list,
            int id,
            NavigationBar navigationBar = null, 
            Globals.ProcessMilestone currentProcessMilestone = default(Globals.ProcessMilestone))
        {
            base.Init(navigationBar, currentProcessMilestone);
            ListOfFoodImages = list;
            MealElementId = id;
            return this;
        }
    }
}
