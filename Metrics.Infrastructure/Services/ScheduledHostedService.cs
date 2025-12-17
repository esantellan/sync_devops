
using Metrics.Api.Configuration;
using Metrics.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Metrics.Infrastructure.Services
{
    public class ScheduledHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<ScheduledHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly RecurringTaskSettings _recurringTaskSettings;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private Timer _timer;

        public ScheduledHostedService(IServiceProvider serviceProvider, ILogger<ScheduledHostedService> logger, IOptions<RecurringTaskSettings> recurringTaskSettings)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _recurringTaskSettings = recurringTaskSettings.Value;

            if (_recurringTaskSettings.Enabled)
            {
                _schedule = CrontabSchedule.Parse(_recurringTaskSettings.CronExpression);
                _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_recurringTaskSettings.Enabled)
            {
                _logger.LogInformation("Recurring task is disabled.");
                return Task.CompletedTask;
            }

            _logger.LogInformation("Scheduled Hosted Service is starting.");

            var now = DateTime.Now;
            var timeUntilNextRun = _nextRun > now ? _nextRun - now : TimeSpan.Zero;

            _timer = new Timer(DoWork, null, timeUntilNextRun, Timeout.InfiniteTimeSpan);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Executing recurring task.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var taskService = scope.ServiceProvider.GetRequiredService<IBackgroundTaskService>();
                taskService.CreateTask(_recurringTaskSettings.Type, _recurringTaskSettings.Arguments).Wait();
            }

            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            var timeUntilNextRun = _nextRun - DateTime.Now;
            _timer.Change(timeUntilNextRun, Timeout.InfiniteTimeSpan);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Scheduled Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
