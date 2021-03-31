using GloboDiet.Services;
using GloboDiet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{

    public class Interviewer : _ModelBase
    {

        [StringLength(30)]
        public string GivenName { get; set; }

        // needed for dropdown
        [NotMapped]
        public new string Label { get => ToString(); }

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



        public static implicit operator Interviewer(InterviewerCreateEdit viewModel) => new Interviewer
        {
            Id = viewModel.Id,
            Code = viewModel.Code,
            GivenName = viewModel.GivenName,
            Name = viewModel.Name,
        };
    }
}
