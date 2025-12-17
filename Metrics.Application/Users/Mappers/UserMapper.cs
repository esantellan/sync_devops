using AutoMapper;
using Metrics.Application.Users.DTOs;
using Metrics.Domain.Entities;

namespace Metrics.Application.Users.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>();
        }
    }
}
