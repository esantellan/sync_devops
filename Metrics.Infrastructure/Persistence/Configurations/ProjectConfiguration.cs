using Metrics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrics.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever(); // The ID comes from Azure DevOps

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(p => p.Name);
        }
    }
}
