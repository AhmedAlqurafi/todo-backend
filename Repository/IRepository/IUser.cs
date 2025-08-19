using backend.Models.DTO.UserDTO;

namespace backend.Repository.IRepository
{
    public interface IUserRepository
    {

        Task AddUser(User user);
        Task UpdateUser(int Id, UserUpdateDTO userUpdateDTO);
        Task<UserGetDTO> GetUserById(int Id);
        Task<UserGetDTO> GetMe(string jwtToken);
        Task DeleteUser(int Id);
    }
}