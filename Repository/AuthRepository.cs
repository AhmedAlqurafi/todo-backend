
using AutoMapper;
using backend.Dto.AuthDto;
using backend.Models.DTO.AuthDto;
using backend.Repository.IRepository;

namespace backend.Repository
{

    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(ApplicationDbContext db, IMapper mapper, ILogger<AuthRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
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

        public Task<LoginRequestDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            _logger.LogInformation("Repo");
            User user = _mapper.Map<User>(registrationRequestDTO);
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

    }
}