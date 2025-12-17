using Metrics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrics.Infrastructure.Persistence.Configurations
{
    public class DevOpsUserConfiguration : IEntityTypeConfiguration<DevOpsUser>
    {
        public void Configure(EntityTypeBuilder<DevOpsUser> builder)
        {
            builder.ToTable("DevOpsUsers");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever(); // The ID comes from Azure DevOps

            builder.Property(u => u.DisplayName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.UniqueName)
                .HasMaxLength(256);

            builder.HasIndex(u => u.UniqueName).IsUnique();
        }
    }
}
