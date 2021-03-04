using GloboDiet.Services;
using GloboDiet.ViewModels;
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

        //public static implicit operator InterviewerCreateEdit(Interviewer interviewer) => new InterviewerCreateEdit
        //{ Id = interviewer.Id, GivenName = interviewer.GivenName, Name = interviewer.Name };

        //public static implicit operator Interviewer(InterviewerCreateEdit interviewerCreateEdit) => new Interviewer
        //{ Id = interviewerCreateEdit.Id, GivenName = interviewerCreateEdit.GivenName, Name = interviewerCreateEdit.Name };

        public InterviewerCreateEdit ToViewModel(NavigationBar navigationBar) => new InterviewerCreateEdit()
        {
            Id = this.Id,
            Code = this.Code,
            GivenName = this.GivenName,
            Name = this.Name,
            NavigationBar = navigationBar
        };

    }
}
