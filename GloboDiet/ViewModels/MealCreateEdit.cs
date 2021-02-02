using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class MealCreateEdit : _ViewModelBase
    {
        public Meal Meal { get; set; }

        public MealCreateEdit(Meal meal, NavigationBar navigationBar)
        {
            Meal = meal;
            NavigationBar = navigationBar;
        }
    }
}
