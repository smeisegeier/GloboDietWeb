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
        private Interview _interview { get; set; }

        public IEnumerable<SelectListItem> DropdownInterviewers { get; }
        public IEnumerable<SelectListItem> DropdownLocations { get; }

        public InterviewCreateEdit(Interview interview,
            IEnumerable<Interviewer> listOfInterviewers,
            IEnumerable<Location> listOfLocations,
            Globals.ProcessMilestone currentProcessMilestone,
            NavigationBar navigationBar)
        {
            _interview = interview;
            DropdownInterviewers = new SelectList(listOfInterviewers, "Id", "Label");
            DropdownLocations = new SelectList(listOfLocations, "Id", "Label");
            CurrentProcessMilestone = currentProcessMilestone;
            NavigationBar = navigationBar;
        }

        public InterviewCreateEdit() { }

        public int Id { get => _interview.Id; }

        [DisplayFormat(NullDisplayText = "Label is null")]
        public string Label { get => _interview.Label; }

        public DateTime Timestamp
        {
            get => _interview.Timestamp;
            set { _interview.Timestamp = value; }
        }

        public int Number
        {
            get => _interview.Number;
            set { _interview.Number = value; }
        }
        [DataType(DataType.Date)]
        public DateTime ReferenceDate
        {
            get => _interview.ReferenceDate;
            set { _interview.ReferenceDate = value; }
        }

        [Display(Name = "Location")]
        public int? LocationId
        {
            get => _interview.LocationId;
            set { _interview.LocationId = value; }
        }
        [Display(Name = "Interviewer")]
        public int? InterviewerId
        {
            get => _interview.InterviewerId;
            set { _interview.InterviewerId = value; }
        }

        public int? RespondentId => _interview.RespondentId;

        [Display(Name = "Active Respondent")]
        //[DisplayFormat(NullDisplayText = "is null")] // still not working
        public string RespondentLabel => string.IsNullOrEmpty(_interview.Respondent?.Label) ? "empty" : _interview.Respondent?.Label;

        public IEnumerable<Meal> Meals 
        { 
            get => _interview.Meals;
            set { _interview.Meals = value; } 
        }
    }
}
