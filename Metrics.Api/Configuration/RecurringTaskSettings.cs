
namespace Metrics.Api.Configuration
{
    public class RecurringTaskSettings
    {
        public bool Enabled { get; set; }
        public string Type { get; set; }
        public string Arguments { get; set; }
        public string CronExpression { get; set; }
    }
}
