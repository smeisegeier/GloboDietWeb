using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Interview
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        public int Number { get; set; } = 1;
        public DateTime ReferenceDate { get; set; } = DateTime.Now;
        public Location Location { get; set; }

        public Interview()
        {

        }
    }
}
