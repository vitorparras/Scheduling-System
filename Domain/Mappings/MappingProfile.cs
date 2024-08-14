using AutoMapper;
using Domain.DTO;
using Domain.Model;

namespace Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
