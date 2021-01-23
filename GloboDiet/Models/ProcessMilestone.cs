using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
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
}
