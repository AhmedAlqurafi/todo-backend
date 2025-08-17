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

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registerDTO)
        {
            var isUnique = _authRepo.IsUniqueUser(registerDTO.Username);
            if (isUnique)
            {
                User user = await _authRepo.Register(registerDTO);
                Console.WriteLine("Register" + registerDTO);
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}