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

        // make the current one more prominent
        public Globals.ProcessMilestone? CurrentProcessMilestone { get; set; }

        public _ViewModelBase(NavigationBar navigationBar)
        {
            NavigationBar = navigationBar;
        }

        // parameterless for modelbinder calls. All displayelements are empty / default
        public _ViewModelBase()
        {
            NavigationBar = new NavigationBar(0, 0, 0, 0, EfCoreHelper.SqlConnectionType.UNKNOWN);
        }

        //public _ViewModelBase() { }

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}
