using backend.Dto.AuthDto;
using backend.Models.DTO.AuthDto;
using backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthAPIControllers : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly ILogger<AuthAPIControllers> _logger;

        public AuthAPIControllers(IAuthRepository authRepo, ILogger<AuthAPIControllers> logger)
        {
            _authRepo = authRepo;
            _logger = logger;
        }

        [HttpGet("blah")]
        public IActionResult Test()
        {
            return Ok("Blah");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {

            if (loginRequestDTO.Username == null || loginRequestDTO.Password == null)
            {
                return BadRequest("No nulls");
            }
            LoginResponseDTO response = await _authRepo.Login(loginRequestDTO);

            return Ok(response);
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registerDTO)
        {
            // Check if password field equalls confirm password field
            var isPasswordConfirmed = registerDTO.Password == registerDTO.ConfirmPassword ? true : false;

            if (!isPasswordConfirmed)
            {
                return BadRequest("Password and Confirmed password does not match");
            }

            // Check if username not used
            var isUnique = _authRepo.IsUniqueUser(registerDTO.Username);
            if (isUnique && isPasswordConfirmed)
            {
                //TODO: Change LoginResponseDTO to RegisterResponseDTO
                LoginResponseDTO user = await _authRepo.Register(registerDTO);
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}