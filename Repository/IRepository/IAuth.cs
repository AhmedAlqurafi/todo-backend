using backend.Dto.AuthDto;
using backend.Models.DTO.AuthDto;

namespace backend.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        bool IsUniqueUser(string username);

    }
}