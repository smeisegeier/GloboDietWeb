using DextersLabor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class _ViewModelBase
    {
        public NavigationBar NavigationBar { get; set; }

        public Globals.ProcessMilestone? CurrentProcessMilestone { get; set; }

        public int Id { get; set; }
        public string Name { get; set; } = "NotSet";
        public string Description { get; set; } = "NotSet";

        [Required(ErrorMessage = "Enter code")]
        public string Code { get; set; } = "00";

        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? UpdatedAt { get; set; }

        public string Label => $"[{Id} | {Name}]";

        // returns this to allow chaining
        public _ViewModelBase Init(NavigationBar navigationBar = null, Globals.ProcessMilestone currentProcessMilestone = default(Globals.ProcessMilestone))
        {
            NavigationBar = navigationBar?? NavigationBar.GetEmptyNavigationBar();
            CurrentProcessMilestone = currentProcessMilestone;
            return this;
        }
    }
}
