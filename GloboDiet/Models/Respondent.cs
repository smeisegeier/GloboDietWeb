using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Services;
using GloboDiet.ViewModels;
using DextersLabor;
namespace GloboDiet.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Respondent : _ModelBase
    {
        public string GivenName { get; set; }

        public double Age { get => Math.Round((DateTime.Now - DateOfBirth).TotalDays / 365.242199, 1); }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTimeHelper.GetDateTimeFromString("1980-01-01", "yyyy-MM-dd");

        public int Height { get; set; } = 175;

        public int Weight { get; set; } = 80;

        [ForeignKey("Interview")]
        public int InterviewId { get; set; }
        public virtual Interview Interview { get; set; }

        public Respondent() { }
        public Respondent(int interviewId) { InterviewId = interviewId; }


        public static IList<Respondent> GetSeedsFromMockup()
        {
            return new List<Respondent>()
                {
                    new Respondent() { Name = "Simpson", GivenName = "Meredith", Code="1700025",Height=165, Weight=70, DateOfBirth=DateTimeHelper.GetDateTimeFromString("1970-05-13", "yyyy-MM-dd"), Gender=Gender.Female},
                    new Respondent() { Name = "lolman", GivenName = "Gary", Code="DA12-B01",Height=182, Weight=98, DateOfBirth=DateTimeHelper.GetDateTimeFromString("1988-11-07", "yyyy-MM-dd"), Gender=Gender.Male}
                };
        }

        //public RespondentCreateEdit ToViewModel(NavigationBar navigationBar, Globals.ProcessMilestone processMilestone) => new RespondentCreateEdit
        //{
        //    Id = this.Id,
        //    GivenName = this.GivenName,
        //    Age = this.Age,
        //    Code = this.Code,
        //    DateOfBirth = this.DateOfBirth,
        //    Gender = this.Gender,
        //    Height = this.Height,
        //    InterviewId = this.InterviewId,
        //    Name = this.Name,
        //    Weight = this.Weight,
            
        //    NavigationBar = navigationBar,
        //    CurrentProcessMilestone = processMilestone
        //};

        public static implicit operator Respondent(RespondentCreateEdit viewModel) => new Respondent
        {
            Id = viewModel.Id,
            GivenName = viewModel.GivenName,
            Code = viewModel.Code,
            DateOfBirth = viewModel.DateOfBirth,
            Gender = viewModel.Gender,
            Height = viewModel.Height,
            InterviewId = viewModel.InterviewId,
            Name = viewModel.Name,
            Weight = viewModel.Weight
        };

    }
}
