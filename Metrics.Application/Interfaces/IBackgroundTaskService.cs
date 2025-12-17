
using Metrics.Domain.Entities;
using System.Threading.Tasks;

namespace Metrics.Application.Interfaces
{
    public interface IBackgroundTaskService
    {
        Task<BackgroundTask> CreateTask(string type, string arguments);
        Task<BackgroundTask> GetQueuedTask();
        Task UpdateTaskStatus(BackgroundTask task, string status);
    }
}
