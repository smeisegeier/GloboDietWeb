using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    /// <summary>
    /// All model classes now inherit from base, which itself is *not* listed.
    /// EF auto-detects inheritence, nothing to configure
    /// </summary>
    public abstract class Base : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
        public Guid Guid { get; set; }
    }
}
