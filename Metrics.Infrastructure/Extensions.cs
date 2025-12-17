using Metrics.Infrastructure.AzureDevOps;
using Metrics.Infrastructure.AzureDevOps.Interfaces;
using Metrics.Infrastructure.Persistence;
using Metrics.Infrastructure.Persistence.Interfaces;
using Metrics.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metrics.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            #endregion

            #region Azure DevOps Client
            services.AddHttpClient<IAzureDevOpsClient, AzureDevOpsClient>();
            #endregion

            return services;
        }
    }
}
