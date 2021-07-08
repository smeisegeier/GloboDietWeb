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
using System.Xml.Serialization;

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

        [XmlIgnore]
        [ForeignKey("Interview")]
        public int InterviewId { get; set; }

        [XmlIgnore]
        public virtual Interview Interview { get; set; }

        public Respondent() { }
        public Respondent(int interviewId) { InterviewId = interviewId; }


        public static IList<Respondent> GetSeedsFromMockup()
        {
            return new List<Respondent>()
                {
                    new Respondent()
                    {
                        GivenName="lol",
                        Gender = GloboDiet.Models.Gender.Female,
                        DateOfBirth = new DateTime(1980, 5,17),
                        Height=178,
                        Weight=80,
                        InterviewId=1,
                        Id=1,
                        Name="xde",
                        Description="-- desc --",
                        Code="07",
                        CreatedAt=DateTime.Now
                    }
                };
        }

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
