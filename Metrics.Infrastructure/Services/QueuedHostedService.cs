
using Metrics.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Metrics.Infrastructure.Services
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger<QueuedHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public QueuedHostedService(IServiceProvider serviceProvider, ILogger<QueuedHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var taskService = scope.ServiceProvider.GetRequiredService<IBackgroundTaskService>();
                    var handlerResolver = scope.ServiceProvider.GetRequiredService<IBackgroundTaskHandlerResolver>();

                    var task = await taskService.GetQueuedTask();

                    if (task != null)
                    {
                        try
                        {
                            var handler = handlerResolver.Resolve(task.Type);
                            await handler.Handle(task.Arguments);

                            await taskService.UpdateTaskStatus(task, "Completed");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error occurred executing task.");
                            await taskService.UpdateTaskStatus(task, "Failed");
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Esperar 5 segundos antes de buscar nuevas tareas
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
