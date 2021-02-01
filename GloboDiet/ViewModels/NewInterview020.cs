using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class NewInterview020 : _ViewModelBase
    {
        public Interview Interview { get; set; }

        public IEnumerable<SelectListItem> ListOfInterviewers { get; set; }
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }
        public IEnumerable<SelectListItem> ListOfRespondents { get; set; }

        // make the current one more prominent
        public Globals.ProcessMilestone CurrentProcessMilestone { get; set; }

        /// <summary>
        /// Constructs new ViewModel
        /// </summary>
        /// <param name="interview"></param>
        /// <param name="listOfInterviewers"></param>
        /// <param name="listOfLocations"></param>
        /// <param name="listOfRespondents"></param>
        /// <param name="currentProcessMilestone"></param>
        /// <param name="navigationBar"></param>
        public NewInterview020(Interview interview,
            IEnumerable<Interviewer> listOfInterviewers,
            IEnumerable<Location> listOfLocations,
            IEnumerable<Respondent> listOfRespondents,
            Globals.ProcessMilestone currentProcessMilestone,
            NavigationBar navigationBar) : base(navigationBar)
        {
            Interview = interview;
            ListOfInterviewers = new SelectList(listOfInterviewers, "Id", "Label");
            ListOfLocations = new SelectList(listOfLocations, "Id", "Label");
            ListOfRespondents = new SelectList(listOfRespondents, "Id", "Label");
            CurrentProcessMilestone = currentProcessMilestone;
        }


    }
}
