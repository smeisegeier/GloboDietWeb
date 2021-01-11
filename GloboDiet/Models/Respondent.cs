using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HelperLibrary;
namespace GloboDiet.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Respondent
    {
        public int Id { get; set; }

        [DisplayName("Respondent Code")]
        public string Code { get; set; }

        [DisplayName("Given Name")]
        public string GivenName { get; set; }

        public string Name { get; set; }

        public float Age { get; set; } // TODO get routine

        public Gender Gender{ get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name="Height in cm", Prompt ="150 - 230 cm")]
        [Required(ErrorMessage ="Height must be provided")]
        [Range(150, 230)]
        public int Height { get; set; }

        [Display(Name="Weight in kg", Prompt ="30 - 300 kg")]
        [Required(ErrorMessage = "Weight must be provided")]
        [Range(30, 300)]
        public int Weight { get; set; }
        public List<Interview> Interviews { get; set; }

        public Respondent()
        {

        }
            public static List<Respondent> GenerateDefaultValues()
            {
                return new List<Respondent>()
                {
                    new Respondent() { Name = "Simpson", GivenName = "Meredith", Age=34.8f, Code="1700025",Height=165, Weight=70, DateOfBirth=DateTimeHelper.GetDateTimeFromString("1970-05-13", "yyyy-MM-dd"), Gender=Gender.Female},
                    new Respondent() { Name = "lolman", GivenName = "Gary", Age = 52.1f, Code="DA12-B01",Height=182, Weight=98, DateOfBirth=DateTimeHelper.GetDateTimeFromString("1988-11-07", "yyyy-MM-dd"), Gender=Gender.Male}
                };
            }

    }
}
