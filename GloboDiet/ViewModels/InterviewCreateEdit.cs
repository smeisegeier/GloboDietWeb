using GloboDiet.Controllers;
using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit : _ViewModelBase
    {
        // Interviewr and Locations are NO lookup data
        public IEnumerable<SelectListItem> DropdownInterviewers { get; set; }
        public IEnumerable<SelectListItem> DropdownLocations { get; set; }


        public void Init(
            IEnumerable<Interviewer> listOfInterviewers,
            IEnumerable<Location> listOfLocations, 
            NavigationBar navigationBar = null, 
            Globals.ProcessMilestone currentProcessMilestone = default(Globals.ProcessMilestone)
            )
        {
            DropdownInterviewers = new SelectList(listOfInterviewers, "Id", "Label");
            DropdownLocations = new SelectList(listOfLocations, "Id", "Label");
            base.Init(navigationBar, currentProcessMilestone);
        }

        public DateTime Timestamp { get; set; }

        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReferenceDate { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "Interviewer")]
        public int? InterviewerId { get; set; }

        public int? RespondentId { get; set; }

        [Display(Name = "Active Respondent")]
        public string RespondentLabel { get; set; } 

        public IList<MealCreateEdit> Meals { get; set; }

        public bool IsCachedOnly { get; set; } = true;

        public static implicit operator InterviewCreateEdit(Interview model)
        {
            var viewModel = new InterviewCreateEdit
            {
                Id = model.Id,
                InterviewerId = model.InterviewerId,
                LocationId = model.LocationId,
                Number = model.Number,
                RespondentId = model.RespondentId,
                ReferenceDate = model.ReferenceDate,
                Timestamp = model.Timestamp,
                Meals = new List<MealCreateEdit>(),
                IsCachedOnly = model.IsCachedOnly,

                RespondentLabel = model.Respondent is null ? "empty" : model.Respondent.ToString()
            };
            model.Meals?.ToList().ForEach(item => { viewModel.Meals.Add(item); });
            return viewModel;
        }

    }
}
