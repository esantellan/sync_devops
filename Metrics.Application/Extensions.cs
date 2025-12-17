using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Metrics.Application.Behaviors;
using Metrics.Infrastructure;

namespace Metrics.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddInfrastructureLayer(config);

            return services;
        }
    }
}
