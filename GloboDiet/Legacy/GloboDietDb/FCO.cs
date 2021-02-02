using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Legacy.GloboDietDb
{
    public class FCO : _LegacyBase
    {
        public string FCM_CODE { get; set; }
        public string FCM_NAME { get; set; }
        public string FCM_MODE { get; set; }
        public string FCM_SNAME { get; set; }
        public int FCM_PRINCIPAL { get; set; }
    }
}
