using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Services;
using HelperLibrary;
namespace GloboDiet.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Respondent : _ModelBase
    {
        [DisplayName("Given Name")]
        public string GivenName { get; set; }

        public double Age { get => Math.Round((DateTime.Now - DateOfBirth).TotalDays / 365.242199, 1); }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTimeHelper.GetDateTimeFromString("1980-01-01", "yyyy-MM-dd");

        [Display(Name = "Height in cm", Prompt = "150 - 230 cm")]
        [Required(ErrorMessage = "Height must be provided")]
        [Range(150, 230)]
        public int Height { get; set; } = 175;

        [Display(Name = "Weight in kg", Prompt = "30 - 300 kg")]
        [Required(ErrorMessage = "Weight must be provided")]
        [Range(30, 300)]
        public int Weight { get; set; } = 80;

        // TODO !check Navigation property
        [ForeignKey("Interview")]
        public int InterviewId { get; set; }
        public virtual Interview Interview { get; set; }

        public Respondent() { }

      

        public static IList<Respondent> GetSeedsFromMockup()
        {
            return new List<Respondent>()
                {
                    new Respondent() { Name = "Simpson", GivenName = "Meredith", Code="1700025",Height=165, Weight=70, DateOfBirth=DateTimeHelper.GetDateTimeFromString("1970-05-13", "yyyy-MM-dd"), Gender=Gender.Female},
                    new Respondent() { Name = "lolman", GivenName = "Gary", Code="DA12-B01",Height=182, Weight=98, DateOfBirth=DateTimeHelper.GetDateTimeFromString("1988-11-07", "yyyy-MM-dd"), Gender=Gender.Male}
                };
        }
    }
}
