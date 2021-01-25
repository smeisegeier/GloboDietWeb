using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class InterviewersList : ViewModelBase
    {
        public IEnumerable<Interviewer> Interviewers { get; set; }

        public InterviewersList(IEnumerable<Interviewer> interviewers, NavigationBar navigationBar) : base(navigationBar)
        {
            Interviewers = interviewers;
        }
    }
}
