using AutoMapper;
using backend.Dto.AuthDto;
using backend.Models.DTO.AuthDto;
using backend.Models.DTO.TodoDTO;

namespace backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RegistrationRequestDTO, User>();
            CreateMap<Todo, TodoGetDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            // CreateMap<TodoCreateDTO, TodoDTO>();

        }
    }
}