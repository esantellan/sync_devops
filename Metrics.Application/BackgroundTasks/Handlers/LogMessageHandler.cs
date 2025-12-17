
using Metrics.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Metrics.Application.BackgroundTasks.Handlers
{
    public class LogMessageHandler : IBackgroundTaskHandler
    {
        private readonly ILogger<LogMessageHandler> _logger;

        public LogMessageHandler(ILogger<LogMessageHandler> logger)
        {
            _logger = logger;
        }

        public string Type => "LogMessage";

        public Task Handle(string arguments)
        {
            _logger.LogInformation("LogMessageHandler executed with arguments: {arguments}", arguments);
            return Task.CompletedTask;
        }
    }
}
