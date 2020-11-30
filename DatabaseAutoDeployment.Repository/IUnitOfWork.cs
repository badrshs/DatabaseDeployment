using DatabaseAutoDeployment.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAutoDeployment.Repository
{
    /// <summary>
    /// UnitOfWork interface
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the entity asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Get<T>() where T : class, IBaseEntity;

        /// <summary>
        /// Saves the asynchronously.
        /// </summary>
        /// <returns></returns>
        void Save();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Add<T>(T entity) where T : class, IBaseEntity;

        /// <summary>
        /// Ensures the created asynchronous.
        /// </summary>
        /// <returns></returns>
        void EnsureCreated();

        /// <summary>
        /// Sets the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        void SetConnectionString(string connectionString);


        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();

        /// <summary>
        /// Executes the SQL raw.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        bool ExecuteSqlRaw(string sql);

        /// <summary>
        /// Determines whether this instance [can connect asynchronous].
        /// </summary>
        /// <returns></returns>
        Task<bool> CanConnectAsync();
    }
}