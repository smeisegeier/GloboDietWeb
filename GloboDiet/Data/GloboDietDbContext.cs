using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Models;

namespace GloboDiet.Data
{
    public class GloboDietDbContext : DbContext
    {
        public GloboDietDbContext(DbContextOptions options) : base(options) { }

        public GloboDietDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=GloboDiet;trusted_connection=true;");
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseInMemoryDatabase("Test");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Training>()
        //        .HasIndex(i => i.FileName)
        //        .IsUnique()
        //        .IsClustered(false);
        //    modelBuilder.HasDefaultSchema("kettler");

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Respondent> Respondents { get; set; }
        public DbSet<PlaceOfMeal> PlacesOfMeal { get; set; }

    }
}
