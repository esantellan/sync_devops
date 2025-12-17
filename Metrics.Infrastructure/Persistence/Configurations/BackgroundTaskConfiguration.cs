
using Metrics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrics.Infrastructure.Persistence.Configurations
{
    public class BackgroundTaskConfiguration : IEntityTypeConfiguration<BackgroundTask>
    {
        public void Configure(EntityTypeBuilder<BackgroundTask> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Type).IsRequired();
            builder.Property(t => t.Arguments).IsRequired();
            builder.Property(t => t.Status).IsRequired();
        }
    }
}
