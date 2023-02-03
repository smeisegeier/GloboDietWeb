﻿using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class InterviewsList : _ViewModelBase
    {
        public IEnumerable<Interview> Interviews { get; set; }

        public InterviewsList(IEnumerable<Interview> interviews, NavigationBar navigationBar)
        {
            Interviews = interviews;
            NavigationBar = navigationBar;
            CurrentProcessMilestone = Globals.ProcessMilestone._1_INTERVIEW;
        }
    }
}