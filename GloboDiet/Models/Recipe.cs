using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Recipe : Base
    {
        public string Description { get; set; }

        public static IEnumerable<Recipe> GetSeededValues()
        {
            return new List<Recipe>()
                {
                    new Recipe() { Name = "lekki", Description = "sehr leeeki" },
                    new Recipe() { Name = "böh", Description = "schmeckit nich" },
                };
        }
    }
}
