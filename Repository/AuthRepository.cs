
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using backend.Dto.AuthDto;
using backend.Models.DTO.AuthDto;
using backend.Models.DTO.UserDTO;
using backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Repository
{

    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthRepository> _logger;
        private string secretKey;
        public AuthRepository(ApplicationDbContext db, IMapper mapper, ILogger<AuthRepository> logger, IConfiguration conf)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            secretKey = conf.GetValue<string>("AppSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            User user = _db.Users.FirstOrDefault(user => user.Username == username);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequestDTO.Username.ToLower() && u.Password == loginRequestDTO.Password);

            // Needs imporevement here
            // if (user == null)
            // {
            //     return null;
            // }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = new UserLoggedInDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email
                }
            };

            return loginResponseDTO;

        }

        //TODO: Change LoginResponseDTO to RegisterResponseDTO
        public async Task<LoginResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            _logger.LogInformation("Repo");
            User user = _mapper.Map<User>(registrationRequestDTO);
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();

            // Return token
            // var registeredUser = await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == user.Username.ToLower());

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = new UserLoggedInDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email
                }
            };

            return loginResponseDTO;
        }
    }
}