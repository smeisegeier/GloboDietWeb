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
        public DbSet<PlaceOfMeal> PlacesOfMeal { get; set; }

        public DbSet<User> User { get; set; }



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

                /// <summary>
                /// Custom configuration for dbcontext here
                /// </summary>
                /// <param name="modelBuilder"></param>
                protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                    modelBuilder.Entity<Training>()
                        .HasIndex(i => i.FileName)
                        .IsUnique()
                        .IsClustered(false);
                    modelBuilder.HasDefaultSchema("kettler");

                    base.OnModelCreating(modelBuilder);
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
