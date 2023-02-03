using DextersLabor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Services
{
    public interface IRepositoryNew<TEntity>
    {
        IEnumerable<TEntity> ItemsGetAll();
        TEntity ItemGetById(int id);
        int ItemAdd(TEntity entity);
        int ItemAddOrUpdate(TEntity entity);
        int ItemUpdate(TEntity entity);
        void ItemDelete(TEntity entity);
        void ItemDelete(int id);
        int ItemsGetCount();

        EfCoreHelper.SqlConnectionType GetSqlConnectionType();
    }
    public class RepositoryNew<TEntity> : IRepositoryNew<TEntity> where TEntity : class, IEntity
    {
        protected readonly GloboDietDbContext _context;

        public RepositoryNew(GloboDietDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> ItemsGetAll() => _context.Set<TEntity>().OrderBy(o => o.Id);

        /// <summary>
        /// Saves entity to Db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>freshly created id</returns>
        public int ItemAdd(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        /// <summary>
        /// Saves entity to Db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>id of entity</returns>
        public int ItemAddOrUpdate(TEntity entity)
        {
            if (_context.Set<TEntity>().Contains(entity))
            {
                _context.Set<TEntity>().Update(entity);
            }
            else
            {
                _context.Set<TEntity>().Add(entity);
            }
            _context.SaveChanges();
            return entity.Id;
        }

        public int ItemUpdate(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void ItemDelete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void ItemDelete(int id)
        {
            ItemDelete(ItemGetById(id));
        }

        public TEntity ItemGetById(int id) => _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);

        public int ItemsGetCount() => _context.Set<TEntity>().Count();


        public EfCoreHelper.SqlConnectionType GetSqlConnectionType() => _context.GetSqlConnectionType();

    }
}
