using GloboDiet.Models;
using DextersLabor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
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

        /// <summary>
        /// for displaying all possible milestones, is built onetime from enum
        /// </summary>
        //public static IEnumerable<KeyValuePair<ProcessMilestone, string>> StaticListOfProcessMilestones { get; } = EnumHelper.GetListWithDescription<ProcessMilestone>();

        //public static SelectList StaticListOfMealTypes { get; set; }
        //public static SelectList StaticListOfMealPlaces { get; set; } 
        //public static SelectList StaticListOfBrandnames { get; set; }

        //public static SelectList StaticListOfEssins { get; } = new SelectList(Essin.GetSeedsFromLegacy(), "Id", "Name");

        public static string ToXml(object obj, string root = null)
        {
            XmlSerializer xmlSerializer = new(obj.GetType(), new XmlRootAttribute(root));
            StringWriter sw = new();
            xmlSerializer.Serialize(sw, obj);
            return sw.ToString();
        }
    }
}
