using Metrics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Metrics.Infrastructure.Persistence
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<BackgroundTask> BackgroundTasks => Set<BackgroundTask>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<DevOpsUser> DevOpsUsers => Set<DevOpsUser>();
        public DbSet<WorkItem> WorkItems => Set<WorkItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
