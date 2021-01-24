using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class LocationCreateEdit : ViewModelBase
    {
        public Location Location { get; set; }
        public string ReturningAction { get; set; }

        public LocationCreateEdit(Location location, NavigationBar navigationBar, string returningAction = null) : base(navigationBar)
        {
            Location = location;
            ReturningAction = returningAction;
        }
    }
}
