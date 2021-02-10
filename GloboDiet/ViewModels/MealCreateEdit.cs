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
        private Meal _meal { get; set; }

        public MealCreateEdit(Meal meal, NavigationBar navigationBar)
        {
            _meal = meal;
            NavigationBar = navigationBar;
        }

        public int Id => _meal.Id;
        public int InterviewId => _meal.InterviewId;

        public int StartingHour
        {
            get => _meal.StartingHour;
            set { _meal.StartingHour = value; }
        }
        public int MealTypeId
        {
            get => _meal.MealTypeId;
            set { _meal.MealTypeId = value; }
        }

        public int MealPlaceId
        {
            get => _meal.MealPlaceId;
            set { _meal.MealPlaceId = value; }
        }

    }
}
