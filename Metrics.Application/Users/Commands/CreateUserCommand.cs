using MediatR;

namespace Metrics.Application.Users.Commands
{
    public record CreateUserCommand(string Name, string Email) : IRequest<Guid>;
}
