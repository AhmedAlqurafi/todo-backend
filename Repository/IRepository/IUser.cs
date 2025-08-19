using backend.Models.DTO.UserDTO;

namespace backend.Repository.IRepository
{
    public interface IUserRepository
    {

        Task AddUser(User user);
        Task UpdateUser(int Id);
        Task<UserGetDTO> GetUserById(int Id);
        Task<User> GetMe();
        Task DeleteUser(int Id);
    }
}