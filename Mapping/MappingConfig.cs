using AutoMapper;
using backend.Dto.AuthDto;
using backend.Models.DTO.AuthDto;

namespace backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RegistrationRequestDTO, User>();
        }
    }
}