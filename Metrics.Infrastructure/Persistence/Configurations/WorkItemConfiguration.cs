using Metrics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metrics.Infrastructure.Persistence.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable("WorkItems");

            builder.HasKey(wi => wi.Id);

            builder.Property(wi => wi.Id)
                .ValueGeneratedNever(); // The ID comes from Azure DevOps

            builder.Property(wi => wi.WorkItemType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(wi => wi.Title)
                .IsRequired();

            builder.Property(wi => wi.State)
                .HasMaxLength(100);

            builder.HasIndex(wi => wi.WorkItemType);
            builder.HasIndex(wi => wi.State);
            builder.HasIndex(wi => wi.AssignedToId);

            // Foreign Key to Project
            builder.HasOne(wi => wi.Project)
                .WithMany() // A project can have many work items
                .HasForeignKey(wi => wi.ProjectId)
                .OnDelete(DeleteBehavior.Cascade); // If a project is deleted, delete its work items

            // Foreign Key to DevOpsUser (Assigned To)
            builder.HasOne(wi => wi.AssignedTo)
                .WithMany() // A user can be assigned to many work items
                .HasForeignKey(wi => wi.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull); // If a user is deleted, set AssignedToId to null
        }
    }
}
