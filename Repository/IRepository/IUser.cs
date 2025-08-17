public interface IUserRepository
{

    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int Id);
}