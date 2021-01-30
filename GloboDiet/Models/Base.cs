using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; } = "NotSet";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // TODO calculated column?
        public Guid Guid { get; } = new Guid();

        /* convenience props*/
        [NotMapped]
        public string Label => $"[{Id} | {Name}]";

    }
}
