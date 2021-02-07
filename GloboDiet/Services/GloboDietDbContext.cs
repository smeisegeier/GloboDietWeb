using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GloboDiet.Services
{
    public class GloboDietDbContext : IdentityDbContext
    {
        public GloboDietDbContext(DbContextOptions<GloboDietDbContext> options) : base(options) { }

        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Respondent> Respondents { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Meal> Meals { get; set; }


        /* Lookup Tables*/
        public DbSet<TypeOfMeal> TypesOfMeal { get; set; }
        public DbSet<PlaceOfMeal> PlacesOfMeal { get; set; }


        /* User Manager*/
        public DbSet<User> User { get; set; }


        /// <summary>
        /// Seeding needs to be in context, not in repo - it has to be started 
        /// on app launch (where no rep is present)
        /// </summary>
        public void SeedDb()
        {
            Database.EnsureCreated();
            if (!Set<Interview>().Any()) Set<Interview>().AddRange(Interview.GetSeedsFromMockup());
            if (!Set<Interviewer>().Any()) Set<Interviewer>().AddRange(Interviewer.GetSeedsFromMockup());
            if (!Set<Location>().Any()) Set<Location>().AddRange(Location.GetSeedsFromMockup());
            if (!Set<Respondent>().Any()) Set<Respondent>().AddRange(Respondent.GetSeedsFromMockup());
            if (!Set<Recipe>().Any()) Set<Recipe>().AddRange(Recipe.GetSeedsFromMockup());

            // Saving is isolated now to prevent FK mismatches
            SaveChanges();
        }

        ///// <summary>
        ///// Custom configuration for dbcontext here
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // try to store computed columns
        //    modelBuilder.Entity<Respondent>()
        //        .Property(p => p.Age)
        //        .UsePropertyAccessMode(PropertyAccessMode.Property);

        //    //base.OnModelCreating(modelBuilder);
        //}

        /*
                /// <summary>
                /// This is only needed when NOT using DI, but creating new context()
                /// Not recommended
                /// </summary>
                /// <param name="optionsBuilder"></param>
                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    //optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=GloboDiet;trusted_connection=true;");
                    //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    //optionsBuilder.UseInMemoryDatabase("Test");
                }


        // Test: using dbcontext as repo
        public IEnumerable<TEntity> GetAllEntities<TEntity>() where TEntity : class, IEntity => Set<TEntity>().ToList();
        public TEntity GetEntityById<TEntity>(int id) where TEntity : class, IEntity => GetAllEntities<TEntity>().FirstOrDefault(x => x.Id == id);
        public void AddEntityNoSave<TEntity>(TEntity entity) where TEntity : class, IEntity  => Set<TEntity>().Add(entity);
        public void UpdateEntityNoSave<TEntity>(TEntity entity) where TEntity : class, IEntity => Set<TEntity>().Update(entity);
        public void DeleteEntityNoSave<TEntity>(TEntity entity) where TEntity : class, IEntity => Set<TEntity>().Remove(entity);
        */
    }
}
