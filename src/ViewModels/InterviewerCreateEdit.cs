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

        [StringLength(30)]
        public string GivenName { get; set; }


        public static implicit operator InterviewerCreateEdit(Interviewer model) => new InterviewerCreateEdit
        {
            Id = model.Id,
            Code = model.Code,
            GivenName = model.GivenName,
            Name = model.Name,
        };
    }
}
