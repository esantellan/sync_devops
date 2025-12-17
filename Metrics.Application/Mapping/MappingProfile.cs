using AutoMapper;
using Metrics.Application.Users.DTOs;
using Metrics.Domain.Entities;

namespace Metrics.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
