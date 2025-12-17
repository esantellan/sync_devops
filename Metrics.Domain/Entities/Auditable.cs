using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Domain.Entities
{
    public abstract class Auditable : BaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
