using GloboDiet.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class InterviewCreateEdit
    {
        public IEnumerable<SelectListItem> ListOfInterviews { get; set; }

        public InterviewCreateEdit(IRepository repo)
        {

        }
    }
}
