using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class MealCreateEdit : _ViewModelBase
    {

        public new int Id { get; set; }

        [ForeignKey(nameof(Interview))]
        public int InterviewId { get; set; }

        public int StartingHour { get; set; }
        public int? MealTypeId { get; set; }
        public string MealTypeLabel { get; set; }

        public int? MealPlaceId { get; set; }
        public string MealPlaceLabel { get; set; }

        public IList<MealElement> MealElements { get; set; }

        public static implicit operator MealCreateEdit(Meal model)
        {
            var viewModel = new MealCreateEdit
            {
                Id = model.Id,
                InterviewId = model.InterviewId,
                MealPlaceId = model.MealPlaceId,
                MealTypeId = model.MealPlaceId,
                StartingHour = model.StartingHour,
                MealElements = new List<MealElement>(),

                MealPlaceLabel = model.MealPlace?.ToString(),
                MealTypeLabel = model.MealType?.ToString(),
            };
            model.MealElements?.ToList().ForEach(item => { viewModel.MealElements.Add(item); });
            return viewModel;
        }
    }
}
