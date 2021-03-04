using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class InterviewerCreateEdit : _ViewModelBase
    {
        //public Interviewer Interviewer { get; set; }

        //public InterviewerCreateEdit(Interviewer interviewer, NavigationBar navigationBar) : base(navigationBar)
        //{
        //    Interviewer = interviewer;
        //}

        public int Id { get; set; }
        public string Code { get; set; }

        [StringLength(30)]
        public string GivenName { get; set; }

        public string Name { get; set; }

        //public InterviewerCreateEdit(int id, string givenName, string name, NavigationBar navigationBar) : base(navigationBar)
        //{
        //    Id = id;
        //    GivenName = givenName;
        //    Name = name;
        //}

        //public InterviewerCreateEdit() { }

        public Interviewer ToModel() => new Interviewer
        {
            Id = this.Id,
            Code = this.Code,
            GivenName = this.GivenName,
            Name = this.Name,
        };
    }
}
