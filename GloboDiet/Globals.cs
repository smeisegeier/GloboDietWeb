using HelperLibrary;
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
        public static List<KeyValuePair<ProcessMilestone, string>> ListOfProcessMilestones { get; } = EnumHelper.GetListWithDescription<ProcessMilestone>();


    }
}
