using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Metrics.Application.Projects.Commands;

namespace Metrics.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncProjects()
        {
            await _mediator.Send(new SyncProjectsCommand());
            return Ok();
        }
    }
}
