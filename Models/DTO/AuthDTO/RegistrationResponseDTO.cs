using backend.Models.DTO.UserDTO;

namespace backend.Dto.AuthDto
{
    public class RegistrationResponseDTO
    {
        public UserLoggedInDTO User { get; set; }
        public string Token { get; set; } = "";
    }
}