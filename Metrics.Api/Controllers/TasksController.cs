
using Metrics.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Metrics.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IBackgroundTaskService _taskService;

        public TasksController(IBackgroundTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(string type, [FromBody] string arguments)
        {
            var task = await _taskService.CreateTask(type, arguments);
            return Ok(task);
        }
    }
}
