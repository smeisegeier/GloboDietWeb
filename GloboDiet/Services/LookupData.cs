using GloboDiet.Models;
using DextersLabor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet;
using System.Reflection;

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
        public SelectList DropdownIngredientGroups { get; set; }

        // repo service cannot referenced here, for it has a shorter scope

        /// <summary>
        /// Delivers AssemblyVersion, also from pipe when in azure.
        /// This cannot be outsourced.
        /// </summary>
        public string SoftwareVersion
        {
            get
            {
                string version = string.Format(
                    "{0}.{1}.{2}.{3}",
                    Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(),
                    Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(),
                    Assembly.GetExecutingAssembly().GetName().Version.Build.ToString(),
                    Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString()
                    );
                return version;
            }
        }

        public EfCoreHelper.SqlConnectionType SqlConnectionType { get; set; }
    }
}
