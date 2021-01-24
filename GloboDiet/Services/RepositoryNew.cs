using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Services
{
    public interface IRepositoryNew<TEntity>
    {
        IEnumerable<TEntity> GetAllItems();
        void AddItems(TEntity entity);
        void UpdateItem(TEntity entity);
        void DeleteItem(TEntity entity);
        int GetItemsCount();
        void SeedItems(IEnumerable<TEntity> entities);
    }
    public class RepositoryNew<TEntity> : IRepositoryNew<TEntity> where TEntity : class, IEntity
    {
        private readonly GloboDietDbContext _context;

        public IEnumerable<TEntity> GetAllItems()
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Set<TEntity>();
            }
        }

        public void AddItems(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void UpdateItem(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public void DeleteItem(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int GetItemsCount()
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Set<TEntity>().Count();
            }
        }

        public void SeedItems(IEnumerable<TEntity> entities)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Set<TEntity>().AddRange(entities);
            }
        }

    }
}
