using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RespondentCreateEdit : ViewModelBase
    {
        public Respondent Respondent { get; set; }

        public RespondentCreateEdit(Respondent respondent, NavigationBar navigationBar) :base(navigationBar)
        {
            Respondent = respondent;
        }
    }
}
