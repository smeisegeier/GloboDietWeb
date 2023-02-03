using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Legacy.GloboDietDb
{
    public class Foods : _LegacyBase
    {
        public string GROUP { get; set; }
        public string SUBGROUP1 { get; set; }
        public string SUBGROUP2 { get; set; }
        public string FOODNUM { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string ORDER { get; set; }
        public int SUPPL { get; set; }
    }
}
