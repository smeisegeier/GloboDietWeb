using HelperLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class _ViewModelBase
    {
        public NavigationBar NavigationBar { get; set; }

        public Globals.ProcessMilestone? CurrentProcessMilestone { get; }

        // standard ctor
        public _ViewModelBase(NavigationBar navigationBar, Globals.ProcessMilestone currentProcessMilestone = default(Globals.ProcessMilestone))
        {
            NavigationBar = navigationBar;
            CurrentProcessMilestone = currentProcessMilestone;
        }

        // parameterless for modelbinder calls / empty displayelements
        public _ViewModelBase() : this(new NavigationBar(0, 0, 0, 0, EfCoreHelper.SqlConnectionType.UNKNOWN))
        {
        }


    }
}
