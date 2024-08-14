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
            
            CreateMap<User, UserAddDTO>()
                .ReverseMap();
        }
    }
}
