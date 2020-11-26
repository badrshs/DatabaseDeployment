using DatabaseAutoDeployment.Entity;
using DatabaseAutoDeployment.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAutoDeployment.Repository
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a database context.
    /// </summary>
    public class FormDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the migration.
        /// </summary>
        /// <value>
        /// The migration.
        /// </value>
        public virtual DbSet<Migration> Migration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public FormDbContext(DbContextOptions<FormDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MigrationConfiguration());
        }
    }
}
