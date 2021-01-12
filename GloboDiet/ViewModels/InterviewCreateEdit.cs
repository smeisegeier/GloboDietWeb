using GloboDiet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit
    {
        public IEnumerable<SelectListItem> ListOfLocations { get; set; }
        public Interview Interview { get; set; }

        public InterviewCreateEdit(Interview interview, List<Location> listOfLocations)
        {
            ListOfLocations = new SelectList(listOfLocations, "Id", "City");
        }
    }
}
