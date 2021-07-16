using GloboDiet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace GloboDiet.Models
{
    public class Meal : _ModelBase
    {
        [Range(0,23)]
        public int StartingHour { get; set; }

        public int? MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public int? MealPlaceId { get; set; }
        public virtual MealPlace MealPlace { get; set; }

        [ForeignKey(nameof(Interview))]
        public int InterviewId { get; set; }

        public virtual List<MealElement> MealElements{ get; set; }

        public bool IsCachedOnly { get; set; } = true;

        public Meal() { }

        // ctor for call w/ parent id
        public Meal(int interviewId) { InterviewId = interviewId; }

        public static implicit operator Meal(MealCreateEdit viewModel)
        {
            var model = new Meal
            {
                Id = viewModel.Id,
                InterviewId = viewModel.InterviewId,
                MealPlaceId = viewModel.MealPlaceId,
                MealTypeId = viewModel.MealPlaceId,
                StartingHour = viewModel.StartingHour,
                MealElements = new List<MealElement>(),
                IsCachedOnly = viewModel.IsCachedOnly
            };
            viewModel.MealElements?.ToList().ForEach(item => { model.MealElements.Add(item); });
            return model;
        }

        public static List<Meal> GetSeedsFromMockup()
        {
            return new List<Meal>()
                {
                    new Meal()
                    {
                        StartingHour=8,
                        MealTypeId=1,
                        MealPlaceId=5,
                        InterviewId=1,
                        Id=1,
                        Name="xde",
                        Description="-- desc --",
                        Code="07",
                        MealElements = MealElement.GetSeedsFromMockup(),
                    }
                };
        }
    }
}
