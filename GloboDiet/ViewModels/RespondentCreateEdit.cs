using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RespondentCreateEdit : _ViewModelBase
    {
        private Respondent _respondent { get; set; }

        public RespondentCreateEdit(Respondent respondent, NavigationBar navigationBar, Globals.ProcessMilestone currentProcessMilestone)
        {
            _respondent = respondent;
            NavigationBar = navigationBar;
            CurrentProcessMilestone = currentProcessMilestone;
        }

        public RespondentCreateEdit() {}

        public int Id { get => _respondent.Id; }

        [DisplayFormat(NullDisplayText = "Label is null")]
        public string Label { get => _respondent.Label; }

        public int InterviewId => _respondent.InterviewId;

        public string Name
        {
            get => _respondent.Name;
            set { _respondent.Name = value; }
        }
        public string Code
        {
            get => _respondent.Code;
            set { _respondent.Code = value; }
        }

        [DisplayName("Given Name")]

        public string GivenName
        {
            get => _respondent.GivenName;
            set { _respondent.GivenName = value; }
        }

        public Gender Gender
        {
            get => _respondent.Gender;
            set { _respondent.Gender= value; }
        }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth
        {
            get => _respondent.DateOfBirth;
            set { _respondent.DateOfBirth = value; }
        }

        public double Age => _respondent.Age;

        [Display(Name = "Height in cm", Prompt = "150 - 230 cm")]
        [Required(ErrorMessage = "Height must be provided")]
        [Range(150, 230)]
        public int Height
        {
            get => _respondent.Height;
            set { _respondent.Height = value; }
        }

        [Display(Name = "Weight in kg", Prompt = "30 - 300 kg")]
        [Required(ErrorMessage = "Weight must be provided")]
        [Range(30, 300)]
        public int Weight
        {
            get => _respondent.Weight;
            set { _respondent.Weight = value; }
        }
    }
}
