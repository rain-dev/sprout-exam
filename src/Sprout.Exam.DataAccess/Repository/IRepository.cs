using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        public Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includes);
        public Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default);
        public Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default, params string[] includes);
        public Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<int> UpdateAsync(TEntity entity, CancellationToken cancellation = default);
        public IQueryable<TEntity> Get(); 
        public Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default);
        public Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default, params string[] includes);
        public TransactionScope BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }

}