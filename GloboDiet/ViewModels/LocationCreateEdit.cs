using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class LocationCreateEdit
    {
        public Location Location { get; set; }
        public string ReturningAction { get; set; }

        public LocationCreateEdit(Location location, string returningAction = null)
        {
            Location = location;
            ReturningAction = returningAction;
        }
    }
}
