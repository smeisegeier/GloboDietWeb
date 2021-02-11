using GloboDiet.Models;
using HelperLibrary;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Services
{
    // TODO use
    /// <summary>
    /// Singleton class
    /// </summary>
    public class LookupData
    {
        public enum ProcessMilestone
        {
            [Description("Interview started")]
            _1_INTERVIEW = 1,

            [Description("_respondent created")]
            _2_RESPONDENT = 2,

            [Description("Meals created")]
            _3_MEALS = 3
        }

        /// <summary>
        /// for displaying all possible milestones, is built onetime from enum
        /// </summary>
        public IEnumerable<KeyValuePair<ProcessMilestone, string>> StaticListOfProcessMilestones { get; } = EnumHelper.GetListWithDescription<ProcessMilestone>();

        public SelectList DropdownMealTypes { get; set; }
        public SelectList DropdownMealPlaces { get; set; }
        public SelectList DropdownBrandnames { get; set; }
    }
}
