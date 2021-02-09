using GloboDiet.Models;
using HelperLibrary;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet
{
    /// <summary>
    /// Contains only application wide constants
    /// </summary>
    public static class Globals
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
        public static IEnumerable<KeyValuePair<ProcessMilestone, string>> StaticListOfProcessMilestones { get; } = EnumHelper.GetListWithDescription<ProcessMilestone>();

        public static SelectList StaticListOfMealTypes { get; set; }
        public static SelectList StaticListOfMealPlaces { get; set; } 
        public static SelectList StaticListOfBrandnames { get; set; }

        //public static SelectList StaticListOfEssins { get; } = new SelectList(Essin.GetSeedsFromLegacy(), "Id", "Name");

    }
}
