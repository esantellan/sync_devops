using AutoMapper;
using Metrics.Application.Users.DTOs;
using Metrics.Application.Users.Queries;
using Metrics.Infrastructure.Persistence.Interfaces;
using MediatR;

namespace Metrics.Application.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }
    }
}
