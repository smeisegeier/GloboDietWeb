using GloboDiet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    /// <summary>
    /// All model classes now inherit from base, which itself is *not* listed.
    /// EF auto-detects inheritence, nothing to configure
    /// </summary>
    public abstract class _ModelBase : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "NotSet";
        public string Description { get; set; } = "NotSet";

        [Required(ErrorMessage = "Enter code")]
        public string Code { get; set; } = "0";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // TODO calculated column?
        public Guid Guid { get; private set; } = new Guid();

        /* convenience props*/
        [NotMapped]
        public string Label => $"[{Code} | {Name}]";

    }
}
