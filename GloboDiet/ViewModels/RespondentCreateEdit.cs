using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RespondentCreateEdit : _ViewModelBase
    {
        public Respondent Respondent { get; set; }

        public RespondentCreateEdit(Respondent respondent, NavigationBar navigationBar, Globals.ProcessMilestone currentProcessMilestone)
        {
            Respondent = respondent;
            NavigationBar = navigationBar;
            CurrentProcessMilestone = currentProcessMilestone;
        }
    }
}
