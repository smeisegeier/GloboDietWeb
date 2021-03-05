using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class LocationCreateEdit : _ViewModelBase
    {
        //public Location Location { get; set; }
        //public string ReturnAction { get; set; }

        //public LocationCreateEdit(Location location, NavigationBar navigationBar, string returnAction = null)
        //{
        //    Location = location;
        //    ReturnAction = returnAction;
        //    NavigationBar = navigationBar;
        //}

        public string City { get; set; }
        public string Country { get; set; }

        public static implicit operator LocationCreateEdit(Location model)
        {
            var viewModel = new LocationCreateEdit
            {
                Id = model.Id,
                City = model.City,
                Country = model.Country
            };
            return viewModel;
        }
    }
}
