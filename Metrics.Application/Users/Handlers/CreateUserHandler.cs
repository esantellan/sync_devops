using Metrics.Application.Users.Commands;
using Metrics.Domain.Entities;
using Metrics.Infrastructure.Persistence.Interfaces;
using MediatR;

namespace Metrics.Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _repo;

        public CreateUserHandler(IUserRepository repo) { _repo = repo; }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email);
            await _repo.AddAsync(user);
            return user.Id;
        }
    }
}
