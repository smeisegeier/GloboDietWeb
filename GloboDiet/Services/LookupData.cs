using GloboDiet.Models;
using HelperLibrary;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet;

namespace GloboDiet.Services
{
    /// <summary>
    /// Singleton class. Comprises all data that are read once at appstart (Lookup tables, const)
    /// </summary>
    public class LookupData
    {
        //public enum ProcessMilestone
        //{
        //    [Description("Interview started")]
        //    _1_INTERVIEW = 1,

        //    [Description("Respondent created")]
        //    _2_RESPONDENT = 2,

        //    [Description("Meals created")]
        //    _3_MEALS = 3
        //}

        /// <summary>
        /// for displaying all possible milestones, is built onetime from enum
        /// </summary>
        public IEnumerable<KeyValuePair<Globals.ProcessMilestone, string>> StaticListOfProcessMilestones { get; } = EnumHelper.GetListWithDescription<Globals.ProcessMilestone>();

        public SelectList DropdownMealTypes { get; set; }
        public SelectList DropdownMealPlaces { get; set; }
        public SelectList DropdownBrandnames { get; set; }
        public SelectList DropdownIngredients { get; set; }
    }
}
