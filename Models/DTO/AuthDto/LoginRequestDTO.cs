using System.ComponentModel.DataAnnotations;

namespace backend.Dto.AuthDto
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}