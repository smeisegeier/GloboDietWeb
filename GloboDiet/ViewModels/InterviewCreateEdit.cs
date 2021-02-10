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
        // TODO cleanup
        public Interview Interview { get; set; }

        public IEnumerable<SelectListItem> ListOfInterviewers { get; set; }
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }

        public IEnumerable<Meal> ListOfMeals { get; set; }


        public InterviewCreateEdit(Interview interview,
            IEnumerable<Interviewer> listOfInterviewers,
            IEnumerable<Location> listOfLocations,
            IEnumerable<Meal> listOfMeals,
            Globals.ProcessMilestone currentProcessMilestone,
            NavigationBar navigationBar)
        {
            Interview = interview;
            ListOfInterviewers = new SelectList(listOfInterviewers, "Id", "Label");
            ListOfLocations = new SelectList(listOfLocations, "Id", "Label");

            ListOfMeals = listOfMeals;
            CurrentProcessMilestone = currentProcessMilestone;
            NavigationBar = navigationBar;
        }

        public InterviewCreateEdit() { }

        public int Id { get => Interview.Id; }

        [DisplayFormat(NullDisplayText = "Label is null")]
        public string Label { get => Interview.Label; }

        public DateTime Timestamp
        {
            get => Interview.Timestamp;
            set { Interview.Timestamp = value; }
        }

        public int Number
        {
            get => Interview.Number;
            set { Interview.Number = value; }
        }
        [DataType(DataType.Date)]
        public DateTime ReferenceDate
        {
            get => Interview.ReferenceDate;
            set { Interview.ReferenceDate = value; }
        }

        [Display(Name = "Location")]
        public int? LocationId
        {
            get => Interview.LocationId;
            set { Interview.LocationId = value; }
        }
        [Display(Name = "Interviewer")]
        public int? InterviewerId
        {
            get => Interview.InterviewerId;
            set { Interview.InterviewerId = value; }
        }

        public int? RespondentId => Interview.RespondentId;

        [Display(Name = "respondent")]
        //[DisplayFormat(NullDisplayText = "is null")] // still not working
        public string RespondentLabel //=> Interview?.Respondent?.Label;
        {
            get
            {
                string x = Interview.Respondent?.Label;
                return string.IsNullOrEmpty(x) ? "empty" : x;
            }
}



    }
}
