using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Interview
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public int Number { get; set; }
        public DateTime ReferenceDate { get; set; }

        public Location Location { get; set; }

        public Interview()
        {

        }
    }
}
