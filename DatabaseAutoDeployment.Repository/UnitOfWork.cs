﻿using DatabaseAutoDeployment.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatabaseAutoDeployment.Repository
{

    /// <summary>
    /// UnitOfWork
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {

        /// <summary>
        /// The disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// The context
        /// </summary>
        private readonly TContext _context;

        /// <summary>
        /// The logger
        /// </summary>
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

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString() => _context.Database.GetConnectionString();

        /// <summary>
        /// Determines whether this instance [can connect asynchronous].
        /// </summary>
        /// <returns></returns>
        public Task<bool> CanConnectAsync() => _context.Database.CanConnectAsync();

        /// <summary>
        /// Gets the entity asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> Get<T>() where T : class, IBaseEntity => _context.Set<T>().Where(e => e.IsDeleted == false).ToList();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Update<T>(T entity) where T : class, IBaseEntity => _context.Update(entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Delete<T>(T entity) where T : class, IBaseEntity => _context.Remove(entity);

        /// <summary>
        /// Softs the delete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void SoftDelete<T>(T entity) where T : class, IBaseEntity
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        /// <summary>
        /// Queries the specified expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IBaseEntity => _context.Set<T>().Where(e => e.IsDeleted == false).Where(expression);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Add<T>(T entity) where T : class, IBaseEntity => _context.Add(entity);

        /// <summary>
        /// Saves the asynchronously.
        /// </summary>
        public void Save() => _context.SaveChanges();

        /// <summary>
        /// Executes the SQL raw.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        //todo: find a way to ExecuteNonQuery from dbContext 
        public bool ExecuteSqlRaw(string sql)
        {
            SqlConnection conn = new SqlConnection(_context.Database.GetConnectionString());
            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(sql);
            return true;
        }

        /// <summary>
        /// Ensures the created asynchronous.
        /// </summary>
        public void EnsureCreated() => _context.Database.EnsureCreated();

        /// <summary>
        /// Migrates the asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task MigrateAsync() => _context.Database.MigrateAsync();

        /// <summary>
        /// Sets the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public void SetConnectionString(string connectionString) => _context.Database.SetConnectionString(connectionString);

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
