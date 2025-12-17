using System.Collections.Generic;
using System.Threading.Tasks;
using Metrics.Infrastructure.AzureDevOps.Models;

namespace Metrics.Infrastructure.AzureDevOps.Interfaces
{
    public interface IAzureDevOpsClient
    {
        Task<IEnumerable<ProjectModel>> GetProjects();
    }
}
