using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Legacy.GloboDietDb
{
    public class GROUPS : _LegacyBase
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
            public string GROUP { get; set; }
            public string NAME { get; set; }
            public string NAME_SHORT { get; set; }
    }
}
