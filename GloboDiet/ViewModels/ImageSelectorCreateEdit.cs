using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class ImageSelectorCreateEdit : _ViewModelBase
    {
        public List<Image> ListOfTestImages { get; set; }

        public int MealElementId { get; set; } 

        public ImageSelectorCreateEdit Init(
            List<Image> list,
            int id,
            NavigationBar navigationBar = null, 
            Globals.ProcessMilestone currentProcessMilestone = default(Globals.ProcessMilestone))
        {
            base.Init(navigationBar, currentProcessMilestone);
            ListOfTestImages = list;
            MealElementId = id;
            return this;
        }
    }
}
