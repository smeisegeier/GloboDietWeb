using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class Interview : _ModelBase
    {

        public DateTime Timestamp { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        public int Number { get; set; } = 1;
        [DataType(DataType.Date)]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Display(Name ="The Location")]
        [Required]
        public int LocationId { get; set; }
        //public Location Location { get; set; }

        public int ResponentId { get; set; }

        public int InterviewerId { get; set; }

        public Interview() { }

        public static IEnumerable<Interview> GetSeedsFromMockup() => new List<Interview>()
        { 
            new Interview()
            {
                Number = 13,
                ReferenceDate = DateTime.Now,
                Timestamp = DateTime.Now
            }
        };
    }
}
