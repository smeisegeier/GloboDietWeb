using GloboDiet.Controllers;
using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit : _ViewModelBase
    {

        public Interview Interview { get; set; }

        public IEnumerable<SelectListItem> ListOfInterviewers { get; set; }
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }
        public IEnumerable<SelectListItem> ListOfRespondents { get; set; }


        /// <summary>
        /// Constructs new ViewModel
        /// </summary>
        /// <param name="interview"></param>
        /// <param name="listOfInterviewers"></param>
        /// <param name="listOfLocations"></param>
        /// <param name="listOfRespondents"></param>
        /// <param name="currentProcessMilestone"></param>
        /// <param name="navigationBar"></param>
        public InterviewCreateEdit(Interview interview,
            IEnumerable<Interviewer> listOfInterviewers,
            IEnumerable<Location> listOfLocations,
            IEnumerable<Respondent> listOfRespondents,
            Globals.ProcessMilestone currentProcessMilestone,
            NavigationBar navigationBar)
        {
            Interview = interview;
            ListOfInterviewers = new SelectList(listOfInterviewers, "Id", "Label");
            ListOfLocations = new SelectList(listOfLocations, "Id", "Label");
            ListOfRespondents = new SelectList(listOfRespondents, "Id", "Label");
            var y = ListOfRespondents.OrderByDescending(o => o.Value).FirstOrDefault().Value;
            ListOfRespondents.OrderByDescending(o => o.Value).FirstOrDefault().Selected = true;

            CurrentProcessMilestone = currentProcessMilestone;
            NavigationBar = navigationBar;
        }


    }
}
