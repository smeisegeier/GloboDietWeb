using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    // This is just a test (EF, repo)
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
        public Guid Guid { get; set; }
    }
}
