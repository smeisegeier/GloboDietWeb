using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class NavigationBar
    {
        public int PillCountInterviews { get; set; }
        public int PillCountInterviewers { get; set; }

        public EfCoreHelper.SqlConnectionType CurrentSqlConnectionType { get; set; }

        public NavigationBar(int pillCountInterviews, int pillCountInterviewers, EfCoreHelper.SqlConnectionType currentSqlConnectionType)
        {
            PillCountInterviews = pillCountInterviews;
            PillCountInterviewers = pillCountInterviewers;
            CurrentSqlConnectionType = currentSqlConnectionType;
        }
    }
}
