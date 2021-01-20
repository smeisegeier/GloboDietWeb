using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet
{
    public enum ProcessMilestone
    {
        _0_NOTSTARTED,
        _1_INTERVIEW,
        _2_RESPONDENT,
        _3_MEALS
    }

    public static class Globals
    {
        public static ProcessMilestone ProcessMilestone { get; set; }

    }
}
