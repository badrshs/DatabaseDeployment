using DatabaseAutoDeployment.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAutoDeployment.Repository.Configurations
{
    internal class MigrationConfiguration : IEntityTypeConfiguration<Migration>
    {
        public void Configure(EntityTypeBuilder<Migration> builder)
        {
            builder.ToTable("Tables", "Migration");

            builder.Property(e => e.ScriptName)
                .IsRequired();
        }
    }
}