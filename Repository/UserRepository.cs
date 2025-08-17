
namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) {
            _db = db;
        }
        public async Task AddUser(User user)
        {
            await _db.AddAsync(user);
        }

        public Task DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}