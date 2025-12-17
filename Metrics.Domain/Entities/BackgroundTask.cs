
using System;

namespace Metrics.Domain.Entities
{
    public class BackgroundTask
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Arguments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string Status { get; set; }
    }
}
