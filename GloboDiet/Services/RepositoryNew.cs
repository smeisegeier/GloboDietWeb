using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Services
{
    public interface IRepositoryNew<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
    public class RepositoryNew<TEntity> : IRepositoryNew<TEntity> where TEntity : class, IEntity
    {
        private readonly GloboDietDbContext _context;
        public IEnumerable<TEntity> GetAll()
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Set<TEntity>();
            }
        }

        public void Add(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
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
    }
}
