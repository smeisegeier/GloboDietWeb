using GloboDiet.Models;
using DextersLabor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace GloboDiet
{
    /// <summary>
    /// Contains only application wide constants
    /// </summary>
    public static class Globals
    {
        // removing this means injecting LookupData in several controllers
        public enum ProcessMilestone
        {
            //[Description("No display")]
            //_0_EMPTY = 0,

            [Description("Interview started")]
            _1_INTERVIEW = 1,

            [Description("Respondent created")]
            _2_RESPONDENT = 2,

            [Description("Meals created")]
            _3_MEALS = 3
        }

        public static readonly string CANCEL = "Cancel";

        public enum ButtonAction
        {
            SAVE_STAY,
            SAVE_LEAVE,
            CANCEL_LEAVE
        }
    }
}
