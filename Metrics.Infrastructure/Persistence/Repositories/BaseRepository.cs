using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Metrics.Infrastructure.Persistence.Interfaces;
using Metrics.Domain.Entities;

namespace Metrics.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity<int>
    {
        protected readonly MainDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(MainDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual T Get(int id)
        {
            return _dbSet.Single(x => x.Id == id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbSet.SingleAsync(x => x.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual T Insert(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id) ?? throw new ArgumentException($"No se encontró la entidad con id '{id}'.");
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
