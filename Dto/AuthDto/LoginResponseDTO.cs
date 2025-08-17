namespace backend.Dto.AuthDto
{
    public class LoginResponseDTO
    {
        public User user { get; set; }
        public string token { get; set; }
    }
}