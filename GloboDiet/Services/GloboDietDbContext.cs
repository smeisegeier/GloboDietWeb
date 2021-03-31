using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GloboDiet.Services
{
    // Update-Database -Context GloboDietDbContext
    public class GloboDietDbContext : DbContext
    {
        private LookupData _lookupData;
        public GloboDietDbContext(DbContextOptions<GloboDietDbContext> options, LookupData lookupData) : base(options) 
        {
            _lookupData = lookupData;
        }

        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Respondent> Respondents { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealElement> MealElements { get; set; }


        /* Lookup Tables*/
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<MealPlace> MealPlaces { get; set; }
        public DbSet<Brandname> Brandnames { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientGroup> IngredientGroups { get; set; }



        /// <summary>
        /// Seeding needs to be in context, not in repo - it has to be started 
        /// on app launch (where no rep is present)
        /// </summary>
        public void SeedDb()
        {
            Database.EnsureCreated();
            /*1) Lookup tables*/
            if (!Set<MealType>().Any()) Set<MealType>().AddRange(MealType.GetSeedsFromLegacy());
            if (!Set<MealPlace>().Any()) Set<MealPlace>().AddRange(MealPlace.GetSeedsFromLegacy());
            if (!Set<Brandname>().Any()) Set<Brandname>().AddRange(Brandname.GetSeedsFromLegacy());
            if (!Set<Ingredient>().Any()) Set<Ingredient>().AddRange(Ingredient.GetSeedsFromLegacy());
            if (!Set<IngredientGroup>().Any()) Set<IngredientGroup>().AddRange(IngredientGroup.GetSeedsFromLegacy());

            /*2) Entites */
            if (!Set<Interviewer>().Any()) Set<Interviewer>().AddRange(Interviewer.GetSeedsFromMockup());
            if (!Set<Location>().Any()) Set<Location>().AddRange(Location.GetSeedsFromMockup());

            // Saving is isolated now to prevent FK mismatches
            SaveChanges();

            /* setup Static selectlists from Lookup */
            _lookupData.DropdownMealTypes = new SelectList(Set<MealType>().ToList(), "Id", "Name");
            _lookupData.DropdownMealPlaces = new SelectList(Set<MealPlace>().ToList(), "Id", "Name");
            _lookupData.DropdownBrandnames = new SelectList(Set<Brandname>().ToList(), "Id", "Name");
            _lookupData.DropdownIngredients = new SelectList(Set<Ingredient>().ToList(), "Id", "Label");
            _lookupData.DropdownIngredientGroups = new SelectList(Set<IngredientGroup>().ToList(), "Id", "Label");
        }

        ///// <summary>
        ///// Custom configuration for dbcontext here
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // try to store computed columns
        //    modelBuilder.Entity<_respondent>()
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
