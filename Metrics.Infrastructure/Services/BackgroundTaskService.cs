
using Metrics.Application.Interfaces;
using Metrics.Domain.Entities;
using Metrics.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Metrics.Infrastructure.Services
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        private readonly MainDbContext _context;

        public BackgroundTaskService(MainDbContext context)
        {
            _context = context;
        }

        public async Task<BackgroundTask> CreateTask(string type, string arguments)
        {
            var task = new BackgroundTask
            {
                Type = type,
                Arguments = arguments,
                Status = "Queued"
            };

            _context.BackgroundTasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<BackgroundTask> GetQueuedTask()
        {
            var task = await _context.BackgroundTasks
                .FirstOrDefaultAsync(t => t.Status == "Queued");

            if (task != null)
            {
                task.Status = "Processing";
                await _context.SaveChangesAsync();
            }

            return task;
        }

        public async Task UpdateTaskStatus(BackgroundTask task, string status)
        {
            task.Status = status;
            _context.BackgroundTasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
