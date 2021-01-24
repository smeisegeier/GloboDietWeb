using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class InterviewerCreateEdit : ViewModelBase
    {
        public Interviewer Interviewer { get; set; }

        public InterviewerCreateEdit(Interviewer interviewer, NavigationBar navigationBar) : base(navigationBar)
        {
            Interviewer = interviewer;
        }
    }
}
