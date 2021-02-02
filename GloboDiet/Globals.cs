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

            [Description("Respondent created")]
            _2_RESPONDENT = 2,

            [Description("Meals created")]
            _3_MEALS = 3
        }

        /// <summary>
        /// for displaying all possible milestones, is built onetime from enum
        /// </summary>
        public static IEnumerable<KeyValuePair<ProcessMilestone, string>> StaticListOfProcessMilestones { get; } = EnumHelper.GetListWithDescription<ProcessMilestone>();

        public static SelectList StaticListOfTypesOfMeal { get; } = new SelectList(TypeOfMeal.GetSeedsFromLegacy(), "Id", "Name");

        public static SelectList StaticListOfPlacesOfMeal { get; } = new SelectList(PlaceOfMeal.GetSeedsFromLegacy(), "Id", "Name");

        public static SelectList StaticListOfBrandnames { get; } = new SelectList(Brandname.GetSeedsFromLegacy(), "Id", "Name");

        public static SelectList StaticListOfEssin { get; } = new SelectList(Essin.GetSeedsFromLegacy(), "Id", "Name");

    }
}
