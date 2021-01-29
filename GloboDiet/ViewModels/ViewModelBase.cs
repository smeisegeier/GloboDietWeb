using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class ViewModelBase
    {
        public NavigationBar NavigationBar { get; set; } 

        public ViewModelBase(NavigationBar navigationBar)
        {
            NavigationBar = navigationBar;
        }

        // parameterless for modelbinder calls. All displayelements are empty / default
        public ViewModelBase()
        {
            NavigationBar = new NavigationBar(0, 0, 0, 0, EfCoreHelper.SqlConnectionType.UNKNOWN);
        }
    }
}
