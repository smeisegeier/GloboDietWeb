using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DextersLabor;
using Z.EntityFramework.Plus;

namespace GloboDiet.Services
{
    // Update-Database -Context GloboDietDbContext
    // Update-Database -Context MyIdentityDbContext

    public class GloboDietDbContext : DbContext
    {
        private LookupData _lookupData;
        public GloboDietDbContext(DbContextOptions<GloboDietDbContext> options, LookupData lookupData) : base(options)
        {
            _lookupData = lookupData;

            /* setup Audit*/
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            {
                (context as GloboDietDbContext).AuditEntries.AddRange(audit.Entries);
                //base.SaveChanges();
            };
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
            base.SaveChanges();

            /* setup Static selectlists from Lookup */
            _lookupData.DropdownMealTypes = new SelectList(Set<MealType>().ToList(), "Id", "Name");
            _lookupData.DropdownMealPlaces = new SelectList(Set<MealPlace>().ToList(), "Id", "Name");
            _lookupData.DropdownBrandnames = new SelectList(Set<Brandname>().ToList(), "Id", "Name");
            _lookupData.DropdownIngredients = new SelectList(Set<Ingredient>().ToList(), "Id", "Label");
            _lookupData.DropdownIngredientGroups = new SelectList(Set<IngredientGroup>().ToList(), "Id", "Label");

            /* setup misc*/
            _lookupData.SqlConnectionType = EfCoreHelper.GetSqlConnectionType(this);

        }

        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }


        //public override int SaveChanges()
        //{
        //    var audit = new Audit();
        //    audit.CreatedBy = "ContentCreator"; //Globals.LoginMitarbeiter?.ToString() ?? "Default";
        //    //audit.Configuration.IgnorePropertyUnchanged = false;
        //    audit.PreSaveChanges(this);
        //    var rowsAffected = base.SaveChanges();
        //    audit.PostSaveChanges();

        //    if (audit.Configuration.AutoSavePreAction != null)
        //    {
        //        audit.Configuration.AutoSavePreAction(this, audit);
        //        base.SaveChanges();
        //    }
        //    return rowsAffected;
        //}


        public void SaveChangesWithAudit()
        {
            var audit = new Audit();
            audit.CreatedBy = "ContentCreator"; //Globals.LoginMitarbeiter?.ToString() ?? "Default";
            this.SaveChanges(audit);
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


        #region generic CRUD
        public IEnumerable<TEntity> ItemsGetAll<TEntity>() where TEntity : class, IEntity => Set<TEntity>().ToList().OrderBy(o => o.Id);
        public TEntity ItemGetById<TEntity>(int id) where TEntity : class, IEntity => ItemsGetAll<TEntity>().FirstOrDefault(x => x.Id == id);

        public int ItemAdd<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Set<TEntity>().Add(entity);
            SaveChangesWithAudit();
            return entity.Id;
        }

        public int ItemUpdate<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Set<TEntity>().Update(entity);
            //Entry(entity).State = EntityState.Modified;
            SaveChangesWithAudit();
            return entity.Id;
        }
        public int ItemAddOrUpdate<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (Set<TEntity>().Contains(entity))
            {
                Set<TEntity>().Update(entity);
            }
            else
            {
                Set<TEntity>().Add(entity);
            }
            SaveChangesWithAudit();
            return entity.Id;
        }

        public void ItemDelete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Set<TEntity>().Remove(entity);
            SaveChangesWithAudit();
        }
        public int ItemsGetCount<TEntity>() where TEntity : class, IEntity => Set<TEntity>().Count();

        #endregion
    }
}
