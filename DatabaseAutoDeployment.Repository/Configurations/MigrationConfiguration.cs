using DatabaseAutoDeployment.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAutoDeployment.Repository.Configurations
{
    internal class MigrationConfiguration : IEntityTypeConfiguration<Migration>
    {
        public void Configure(EntityTypeBuilder<Migration> builder)
        {
            builder.ToTable("Migration", "Migration");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}