using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.UserDTO
{
    public class UserUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}