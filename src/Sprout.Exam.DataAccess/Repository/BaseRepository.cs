using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        protected readonly SproutExamDbContext _dbContext;

        public BaseRepository(SproutExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TransactionScope BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            return _dbContext.BeginTransaction(isolationLevel);
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var exists = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            if (exists is not null)
            {
                _dbContext.Remove(entity);
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return -1;

        }

        public IQueryable<TEntity> Get()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Set<TEntity>()
                .ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);

            return query.ToListAsync(cancellationToken);

        }

        public async Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Set<TEntity>()
                .Where(expr)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default, params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            return await query
                .Where(expr)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(expr);
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default, params string[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            return await query
                .FirstOrDefaultAsync(expr);
        }

        public async Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var exists = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            if (exists is null)
            {
                _dbContext.Add(entity);
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return -1;
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var exists = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            if (exists is not null)
            {
                _dbContext.Update(entity);
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return -1;
        }
    }

}