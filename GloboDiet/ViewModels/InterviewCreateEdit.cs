using GloboDiet.Controllers;
using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit
    {
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }
        public Interview Interview { get; set; }
        public List<string> ListOfProcessMilestones{ get; set; }

    public InterviewCreateEdit(Interview interview, List<Location> listOfLocations, List<string> listOfProcessMilestones)
        {
            Interview = interview;
            ListOfLocations = new SelectList(listOfLocations, "Id", "City");
            ListOfProcessMilestones = listOfProcessMilestones;
        }
    }
}
