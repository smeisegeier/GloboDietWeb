using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RespondentsList : _ViewModelBase
    {
        public IEnumerable<Respondent> Respondents { get; set; }

        public RespondentsList(IEnumerable<Respondent> respondents, NavigationBar navigationBar) : base(navigationBar)
        {
            Respondents = respondents;
        }
    }
}
