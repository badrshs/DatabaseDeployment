using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using DatabaseAutoDeployment.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    /// <summary>
    /// Extension class for services registrations.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Registers the sql database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="connectionString">The configuration.</param>
        /// <param name="serviceLifetime">The service lifetime.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterSqlDatabase<T>(this IServiceCollection services, string connectionString, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where T : DbContext =>
            services.RegisterSqlDatabaseAndUnitOfWork<T>(connectionString, serviceLifetime);

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        /// <summary>
        /// Registers the SQL database and unit of work.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceLifetime">The service lifetime.</param>
        /// <returns></returns>
        private static IServiceCollection RegisterSqlDatabaseAndUnitOfWork<T>(this IServiceCollection services, string connectionString, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where T : DbContext
        {
            services.AddUniteOfWork<T>();

            services.AddDbContext<T>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        /// <summary>
        /// Adds the unite of work.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        private static void AddUniteOfWork<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
        }
    }
}
