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
    /// <summary>
    /// Represents a generic repository interface for data access.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier (usually a struct).</typeparam>
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        /// <summary>
        /// Saves an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>A list of entities.</returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all entities with specified related entities included.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <param name="includes">Related entities to include (e.g., navigation properties).</param>
        /// <returns>A list of entities with related entities included.</returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includes);

        /// <summary>
        /// Retrieves entities based on a filter expression asynchronously.
        /// </summary>
        /// <param name="expr">The filter expression.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>A list of filtered entities.</returns>
        Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves entities based on a filter expression with specified related entities included.
        /// </summary>
        /// <param name="expr">The filter expression.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <param name="includes">Related entities to include (e.g., navigation properties).</param>
        /// <returns>A list of filtered entities with related entities included.</returns>
        Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default, params string[] includes);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellation">Cancellation token (optional).</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellation = default);

        /// <summary>
        /// Gets a queryable representation of the entities.
        /// </summary>
        /// <returns>A queryable collection of entities.</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Retrieves an entity based on a filter expression asynchronously.
        /// </summary>
        /// <param name="expr">The filter expression.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>The filtered entity.</returns>
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an entity based on a filter expression with specified related entities included.
        /// </summary>
        /// <param name="expr">The filter expression.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <param name="includes">Related entities to include (e.g., navigation properties).</param>
        /// <returns>The filtered entity with related entities included.</returns>
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken = default, params string[] includes);

        /// <summary>
        /// Begins a transaction scope.
        /// </summary>
        /// <param name="isolationLevel">Isolation level for the transaction (optional).</param>
        /// <returns>A transaction scope.</returns>
        public TransactionScope BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }

}