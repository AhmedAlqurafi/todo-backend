
using backend.Models.DTO.UserDTO;
using backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task AddUser(User user)
        {
            await _db.AddAsync(user);
        }

        public async Task<UserGetDTO> GetUserById(int Id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user == null)
            {
                throw new Exception();
            }

            UserGetDTO userGetDTO = new UserGetDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email
            }; 

            return userGetDTO;
        }

       

        public Task<User> GetMe()
        {
            throw new NotImplementedException();
        }


        public Task UpdateUser(int Id)
        {
            throw new NotImplementedException();
        }
        public Task DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }
    }
}