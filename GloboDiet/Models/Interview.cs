using GloboDiet.Services;
using GloboDiet.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Interview : _ModelBase
    {

        public DateTime Timestamp { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        public int Number { get; set; } = 13;

        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        public int? InterviewerId { get; set; }

        public int? LocationId { get; set; }

        public int? RespondentId { get; set; }

        // needed to resolve Label for display
        public virtual Respondent Respondent { get; set; }

        public virtual IList<Meal> Meals { get; set; }

        public bool IsCachedOnly { get; set; } = true;

        public Interview() { }

        public static IList<Interview> GetSeedsFromMockup() => new List<Interview>()
        {
            new Interview()
            {
                Number = 13,
                ReferenceDate = DateTime.Now,
                Timestamp = DateTime.Now
            }
        };

        //public InterviewCreateEdit ToViewModel(IEnumerable<Interviewer> listOfInterviewers, IEnumerable<Location> listOfLocations, NavigationBar navigation)
        //{
        //    var vm = new InterviewCreateEdit
        //    {
        //        Id = this.Id,
        //        InterviewerId = this.InterviewerId,
        //        LocationId = this.LocationId,
        //        Number = this.Number,
        //        RespondentLabel = this.Respondent?.Label,
        //        RespondentId = this.RespondentId,
        //        ReferenceDate = this.ReferenceDate,
        //        Timestamp = this.Timestamp,
        //        Meals = this.Meals,
        //        NavigationBar = navigation,
        //    };
        //    vm.InitDropdowns(listOfInterviewers, listOfLocations);
        //    return vm;
        //}

        public static implicit operator Interview(InterviewCreateEdit viewModel)
        {
            var model = new Interview
            {
                Id = viewModel.Id,
                InterviewerId = viewModel.InterviewerId,
                LocationId = viewModel.LocationId,
                Number = viewModel.Number,
                RespondentId = viewModel.RespondentId,
                ReferenceDate = viewModel.ReferenceDate,
                Timestamp = viewModel.Timestamp,
                Meals = new List<Meal>(),
                IsCachedOnly = viewModel.IsCachedOnly 
            };
            viewModel.Meals?.ToList().ForEach(item => { model.Meals.Add(item); });
            return model;
        }

    }
}
