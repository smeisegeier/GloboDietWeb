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
        public int InterviewId { get; set; }

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

        public static implicit operator RespondentCreateEdit(Respondent model) => new RespondentCreateEdit
        {
            Id = model.Id,
            GivenName = model.GivenName,
            Age = model.Age,
            Code = model.Code,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            Height = model.Height,
            InterviewId = model.InterviewId,
            Name = model.Name,
            Weight = model.Weight,
        };

    }
}
