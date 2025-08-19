
using System.IdentityModel.Tokens.Jwt;
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



        public Task<UserGetDTO> GetMe(string jwtToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
            var userId = token.Claims.First(c => c.Type == "Id").Value;
            System.Diagnostics.Debug.WriteLine("Testing");
            return GetUserById(int.Parse(userId));
        }


        public async Task UpdateUser(int Id, UserUpdateDTO userUpdateDTO)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Id);

            // TODO: Implement the login to not found user
            var updatedUser = new User
            {
                Id = user.Id,
                Password = user.Password, // Assuming password is not updated here
                FirstName = userUpdateDTO.FirstName,
                LastName = userUpdateDTO.LastName,
                Username = userUpdateDTO.Username,
                Email = userUpdateDTO.Email
                
            };
            _db.Update(updatedUser);
            await _db.SaveChangesAsync();


        }
        public async Task DeleteUser(int Id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user == null)
            {
                throw new Exception();
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}