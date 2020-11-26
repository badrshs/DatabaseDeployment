using DatabaseAutoDeployment.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatabaseAutoDeployment.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private bool _disposed;
        private readonly TContext _context;
        private readonly ILogger<UnitOfWork<TContext>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">The logger.</param>
        public UnitOfWork(TContext dbContext, ILogger<UnitOfWork<TContext>> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        /// <inheritdoc/>
        public virtual List<T> Get<T>() where T : class, IBaseEntity => _context.Set<T>().Where(e => e.IsDeleted == false).ToList();

        /// <inheritdoc/>
        public virtual List<T> Get<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity => Query(expression).ToList();

        public void CreateDatabase()
        {
            _context.Database.EnsureCreated();
        }

        /// <inheritdoc/>
        public virtual void Add<T>(T entity) where T : class, IBaseEntity => _context.Add(entity);

        /// <inheritdoc/>
        public Task<List<T>> GetAsync<T>() where T : class, IBaseEntity => _context.Set<T>().Where(e => e.IsDeleted == false).ToListAsync();

        /// <inheritdoc/>
        public virtual void Update<T>(T entity) where T : class, IBaseEntity => _context.Update(entity);

        /// <inheritdoc/>
        public virtual void Delete<T>(T entity) where T : class, IBaseEntity => _context.Remove(entity);

        /// <inheritdoc/>
        public Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity =>
            _context.Set<T>().Where(e => e.IsDeleted == false).Where(expression).ToListAsync();

        /// <summary>
        /// Delete the entity in a soft manner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void SoftDelete<T>(T entity) where T : class, IBaseEntity
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        /// <inheritdoc/>
        public virtual T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity =>
            Query(expression).FirstOrDefault();

        /// <inheritdoc/>
        public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, IBaseEntity => Query(expression).FirstOrDefaultAsync();

        /// <inheritdoc/>
        public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity => _context.Set<T>().Where(e => e.IsDeleted == false).Where(expression);

        /// <inheritdoc/>
        public virtual IQueryable<T> Query<T>() where T : class, IBaseEntity => _context.Set<T>().Where(e => e.IsDeleted == false);

        /// <summary>
        /// Check if any entity exist asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity =>
            _context.Set<T>().Where(e => e.IsDeleted == false).AnyAsync(expression);

        /// <inheritdoc/>
        public void Save() => _context.SaveChanges();

        /// <summary>
        /// Saves the asynchronously.
        /// </summary>
        /// <returns></returns>
        public virtual Task SaveAsync() => _context.SaveChangesAsync();

        /// <summary>
        /// Executes the in transaction asynchronously.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public virtual Task ExecuteInTransactionAsync(Func<IUnitOfWork, Task> action) => ExecuteInTransactionAsync(action, IsolationLevel.ReadCommitted);

        /// <summary>
        /// Executes the in transaction asynchronously.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        public virtual Task ExecuteInTransactionAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel)
        {
            IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(isolationLevel))
                {
                    try
                    {
                        // Execute the action itself
                        await action(this);

                        // save changes.
                        await SaveAsync();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Transaction failed while creating an execution strategy.");
                        _logger.LogWarning("Trying to rollback...");
                        transaction.Rollback();
                        _logger.LogWarning("Rollback successfully!");
                        throw;
                    }
                }
            });
        }

        /// <summary>
        /// Adds the range asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity => _context.AddRangeAsync(entities);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _disposed = true;
                _context.Dispose();
            }
        }
    }
}
