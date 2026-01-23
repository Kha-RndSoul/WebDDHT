using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebDDHT.Data;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    /// <summary>
    /// Generic Repository Implementation - Base class cho tất cả repositories
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SchoolSuppliesContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SchoolSuppliesContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ========== READ OPERATIONS ==========

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        // ========== WRITE OPERATIONS ==========

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        // ========== ADVANCED OPERATIONS ==========

        public virtual IEnumerable<T> GetWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public virtual IEnumerable<T> GetPaged(int page, int pageSize, out int totalRecords)
        {
            totalRecords = _dbSet.Count();

            return _dbSet
                .OrderBy(e => e) // EF requires ordering for Skip
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public virtual IEnumerable<T> GetOrdered<TKey>(
            Expression<Func<T, TKey>> orderBy,
            bool ascending = true)
        {
            return ascending
                ? _dbSet.OrderBy(orderBy).ToList()
                : _dbSet.OrderByDescending(orderBy).ToList();
        }
    }
}