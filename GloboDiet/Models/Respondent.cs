using System;
using System.Collections.Generic;
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
        public string Code { get; set; }
        public string GivenName { get; set; }
        public string Name { get; set; }
        public float Age { get; set; } // TODO get routine
        public Gender Gender{ get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Height { get; set; }
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
