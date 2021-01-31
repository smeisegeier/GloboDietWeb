using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{

    public class Interviewer : _ModelBase
    {

        [StringLength(30)]
        public string GivenName { get; set; }
        //public string Name { get; set; }

        public Interviewer() { }
        public static IEnumerable<Interviewer> GetSeedsFromMockup()
        {
            return new List<Interviewer>()
                {
                    new Interviewer() { Name = "Jonas", GivenName = "Justus", Code="D1" },
                    new Interviewer() { Name = "Shaw", GivenName = "Peter", Code="D2"}
                };
        }
    }
}
