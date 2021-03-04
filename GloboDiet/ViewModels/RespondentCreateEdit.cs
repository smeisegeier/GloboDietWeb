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
        //private Respondent _respondent { get; set; }

        //public RespondentCreateEdit(Respondent respondent, NavigationBar navigationBar, Globals.ProcessMilestone currentProcessMilestone) : base(navigationBar, currentProcessMilestone)
        //{
        //    _respondent = respondent;
        //}

        //public RespondentCreateEdit() {}

        public int Id { get; set; }

        [DisplayFormat(NullDisplayText = "Label is null")]
        public string Label { get; set; }

        public int InterviewId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        [DisplayName("Given Name")]
        public string GivenName { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public double Age { get; set; }

        [Display(Name = "Height in cm", Prompt = "150 - 230 cm")]
        [Required(ErrorMessage = "Height must be provided")]
        [Range(150, 230)]
        public int Height { get; set; }

        [Display(Name = "Weight in kg", Prompt = "30 - 300 kg")]
        [Required(ErrorMessage = "Weight must be provided")]
        [Range(30, 300)]
        public int Weight { get; set; }

        public Respondent ToModel() => new Respondent
        {
            Id = this.Id,
            GivenName = this.GivenName,
            Code = this.Code,
            DateOfBirth = this.DateOfBirth,
            Gender = this.Gender,
            Height = this.Height,
            InterviewId = this.InterviewId,
            Name = this.Name,
            Weight = this.Weight,

        };

    }
}
