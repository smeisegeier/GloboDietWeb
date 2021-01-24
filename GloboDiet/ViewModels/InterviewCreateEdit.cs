using GloboDiet.Controllers;
using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit : ViewModelBase
    {
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }
        public Interview Interview { get; set; }

        // for displaying all possible milestones
        public List<KeyValuePair<ProcessMilestone, string>> ListOfProcessMilestones { get; set; }

        // make the current one more prominent
        public ProcessMilestone CurrentProcessMilestone { get; set; }

        public InterviewCreateEdit(Interview interview, IEnumerable<Location> listOfLocations, List<KeyValuePair<ProcessMilestone, string>> listOfProcessMilestones, ProcessMilestone currentProcessMilestone, NavigationBar navigationBar) : base(navigationBar)
        {
            Interview = interview;
            ListOfLocations = new SelectList(listOfLocations, "Id", "City");
            ListOfProcessMilestones = listOfProcessMilestones;
            CurrentProcessMilestone= currentProcessMilestone;
        }
    }
}
