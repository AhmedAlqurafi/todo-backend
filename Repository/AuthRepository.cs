
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private string? secretKey;
        public AuthRepository(ApplicationDbContext db, IMapper mapper, ILogger<AuthRepository> logger, IConfiguration conf)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            secretKey = conf.GetValue<string>("AppSettings:Secret");
        }

        public bool IsUniqueUser(RegistrationRequestDTO registrationRequestDTO)
        {
            User? user = _db.Users.FirstOrDefault(user => user.Username.ToLower() == registrationRequestDTO.Username.ToLower() || user.Email == registrationRequestDTO.Email);

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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequestDTO.Username.ToLower());

            // No user found
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.Password))
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

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

        //TODO: Check token generation method.
        //TODO: Implement a token generator method
        public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            _logger.LogInformation("Repo");
            User user = _mapper.Map<User>(registrationRequestDTO);
            user.Username = user.Username.ToLower().Trim();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);

            await _db.AddAsync(user);
            await _db.SaveChangesAsync();

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
            RegistrationResponseDTO responseDTO = new RegistrationResponseDTO()
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

            return responseDTO;
        }
    }
}