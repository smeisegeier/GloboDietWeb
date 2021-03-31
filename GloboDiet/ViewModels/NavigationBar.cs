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
        public int PillCountLocations { get; set; }
        public int PillCountRespondents { get; set; }

        public EfCoreHelper.SqlConnectionType CurrentSqlConnectionType { get; set; }

        public NavigationBar(int pillCountInterviews, int pillCountInterviewers, int pillCountLocations, int pillCountRespondents, EfCoreHelper.SqlConnectionType currentSqlConnectionType)
        {
            PillCountInterviews = pillCountInterviews;
            PillCountInterviewers = pillCountInterviewers;
            PillCountLocations = pillCountLocations;
            PillCountRespondents = pillCountRespondents;
            CurrentSqlConnectionType = currentSqlConnectionType;
        }

        // cannot DI from repo service since this is no MVC controller
        public NavigationBar() {}

        public static NavigationBar GetEmptyNavigationBar()
            => new NavigationBar(0, 0, 0, 0, EfCoreHelper.SqlConnectionType.UNKNOWN);
    }
}
