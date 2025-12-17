
using Metrics.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Metrics.Application.Handlers
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
            _logger.LogInformation($"Executing background task: {Type} - Arguments: {arguments}");
            return Task.CompletedTask;
        }
    }
}
