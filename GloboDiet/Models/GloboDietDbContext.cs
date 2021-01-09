using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Models
{
    public class GloboDietDbContext : DbContext
    {
        public GloboDietDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Training>()
        //        .HasIndex(i => i.FileName)
        //        .IsUnique()
        //        .IsClustered(false);
        //    modelBuilder.HasDefaultSchema("kettler");

        //    base.OnModelCreating(modelBuilder);
        //}

        //public DbSet<Training> Trainings { get; set; }
        //public DbSet<Record> Records { get; set; }

    }
}
