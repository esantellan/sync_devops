using Metrics.Application.Users.Commands;
using Metrics.Application.Users.DTOs;
using Metrics.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo usuario mediante un Command.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (command == null) return BadRequest("Datos inválidos");
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
        }

        /// <summary>
        /// Obtiene un usuario por su Id mediante un Query.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return user is null ? NotFound() : Ok(user);
        }
    }
}
