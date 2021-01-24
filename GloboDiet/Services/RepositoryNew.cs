﻿using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Services
{
    public interface IRepositoryNew<TEntity>
    {
        IEnumerable<TEntity> GetAllItems();
        void AddItem(TEntity entity);
        void UpdateItem(TEntity entity);
        void DeleteItem(TEntity entity);
        int GetItemsCount();
        void SeedItems(IEnumerable<TEntity> entities);
        Extensions.SqlConnectionType GetSqlConnectionType();
    }
    public class RepositoryNew<TEntity> : IRepositoryNew<TEntity> where TEntity : class, IEntity
    {
        protected readonly GloboDietDbContext _context;

        public RepositoryNew(GloboDietDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAllItems()
        {
            using (var context = new GloboDietDbContext())
            {
                return _context.Set<TEntity>();
            }
        }

        public void AddItem(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
        }

        public void UpdateItem(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                _context.Update(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteItem(TEntity entity)
        {
            using (var context = new GloboDietDbContext())
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new GloboDietDbContext())
            {
                return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int GetItemsCount()
        {
            using (var context = new GloboDietDbContext())
            {
                return _context.Set<TEntity>().Count();
            }
        }

        public void SeedItems(IEnumerable<TEntity> entities)
        {
            if (!_context.Set<TEntity>().Any())
            {
                _context.Set<TEntity>().AddRange(entities);
                _context.SaveChanges();
            }
        }

        public Extensions.SqlConnectionType GetSqlConnectionType() => _context.GetSqlConnectionType();

    }
}
