using backend.Models.DTO.UserDTO;

namespace backend.Repository.IRepository
{
    public interface IUserRepository
    {

        Task AddUser(User user);
        Task UpdateUser(int Id, UserUpdateDTO userUpdateDTO);
        Task<UserGetDTO> GetUserById(int Id);
        Task<UserGetDTO> GetMe(int Id);
        Task ChangePassword(int Id, string currentPassword, string newPassword);
        Task UpdateProfile(int Id, string profileImg);
        Task DeleteUser(int Id);

    }
}