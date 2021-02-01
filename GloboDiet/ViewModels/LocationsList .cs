using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class LocationsList : _ViewModelBase
    {
        public IEnumerable<Location> Locations { get; set; }

        public LocationsList(IEnumerable<Location> locations, NavigationBar navigationBar) : base(navigationBar)
        {
            Locations = locations;
        }
    }
}
