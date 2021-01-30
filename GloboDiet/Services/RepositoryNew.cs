﻿using HelperLibrary;
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
        void ItemAdd(TEntity entity);
        void ItemUpdate(TEntity entity);
        void ItemDelete(TEntity entity);
        int ItemsGetCount();
        void ItemsSeed(IEnumerable<TEntity> entities);
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


        public void ItemAdd(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void ItemUpdate(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public void ItemDelete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity ItemGetById(int id) => _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);

        public int ItemsGetCount() => _context.Set<TEntity>().Count();

        // TODO saving should be isolated!
        public void ItemsSeed(IEnumerable<TEntity> entities)
        {
            if (!_context.Set<TEntity>().Any())
            {
                _context.Set<TEntity>().AddRange(entities);
                _context.SaveChanges();
            }
        }

        public EfCoreHelper.SqlConnectionType GetSqlConnectionType() => _context.GetSqlConnectionType();

    }
}
