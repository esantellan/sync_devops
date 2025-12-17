using Metrics.Application.Users.DTOs;
using MediatR;

namespace Metrics.Application.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
}
