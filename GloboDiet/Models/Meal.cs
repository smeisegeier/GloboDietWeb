using GloboDiet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Meal : _ModelBase
    {
        [Range(0,23)]
        public int StartingHour { get; set; }

        public int MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public int MealPlaceId { get; set; }
        public virtual MealPlace MealPlace { get; set; }

        [ForeignKey(nameof(Interview))]
        public int InterviewId { get; set; }

        public virtual IList<MealElement> MealElements{ get; set; }

        public Meal() { }

        // ctor for call w/ parent id
        public Meal(int interviewId) { InterviewId = interviewId; }

        //public MealCreateEdit ToViewModel(NavigationBar navigationBar) => new MealCreateEdit
        //{
        //    Id = this.Id,
        //    InterviewId = this.InterviewId,
        //    MealPlaceId = this.MealPlaceId,
        //    MealTypeId = this.MealPlaceId,
        //    StartingHour = this.StartingHour,
        //    MealElements = this.MealElements,
        //    NavigationBar = navigationBar
        //};


        public static implicit operator Meal(MealCreateEdit viewModel)
        {
            var model = new Meal
            {
                Id = viewModel.Id,
                InterviewId = viewModel.InterviewId,
                MealPlaceId = viewModel.MealPlaceId,
                MealTypeId = viewModel.MealPlaceId,
                StartingHour = viewModel.StartingHour,
                MealElements = new List<MealElement>()
            };
            viewModel.MealElements?.ToList().ForEach(item => { model.MealElements.Add(item); });
            return model;
        }
    }
}
