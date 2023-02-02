using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class ImageSelector : _ViewModelBase
    {
        public List<Image> ListOfTestImages { get; set; }

        public ImageSelector Init(
            List<Image> list,
            NavigationBar navigationBar = null, 
            Globals.ProcessMilestone currentProcessMilestone = default(Globals.ProcessMilestone))
        {
            base.Init(navigationBar, currentProcessMilestone);
            ListOfTestImages = list;
            return this;
        }
    }
}
