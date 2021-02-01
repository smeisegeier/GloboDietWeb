using GloboDiet.Controllers;
using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit : _ViewModelBase
    {
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }
        public Interview Interview { get; set; }


        // make the current one more prominent
        public Globals.ProcessMilestone CurrentProcessMilestone { get; set; }

        public InterviewCreateEdit(Interview interview, IEnumerable<Location> listOfLocations, Globals.ProcessMilestone currentProcessMilestone, NavigationBar navigationBar) : base(navigationBar)
        {
            Interview = interview;
            ListOfLocations = new SelectList(listOfLocations, "Id", "Label");
            CurrentProcessMilestone= currentProcessMilestone;
        }
    }
}
