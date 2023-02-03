using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Legacy.GloboDietDb
{
    public class Anthrop : _LegacyBase
    {
        public string ANT_VAR { get; set; }
        public int AGE_MIN { get; set; }
        public int AGE_MAX { get; set; }
        public int ANT_MIN { get; set; }
        public int ANT_MAX { get; set; }
        public int ANT_DEF { get; set; }
        public int SEX { get; set; }

        public static List<Anthrop> GetAnthro()
        {
            string s = System.IO.File.ReadAllText("Legacy/Anthrop.json");
            return JsonConvert.DeserializeObject<List<Anthrop>>(s);
        }
    }
}
