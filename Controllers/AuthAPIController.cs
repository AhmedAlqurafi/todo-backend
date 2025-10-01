using System.Net;
using backend.Dto.AuthDto;
using backend.Models;
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
        protected APIResponse _response;

        public AuthAPIControllers(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
            this._response = new();
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
            // Check if username or password is empty
            if (string.IsNullOrEmpty(loginRequestDTO.Username) || string.IsNullOrEmpty(loginRequestDTO.Password))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username and Password are required");
                return BadRequest(_response);
            }

            LoginResponseDTO response = await _authRepo.Login(loginRequestDTO);
            if (response.User == null || string.IsNullOrEmpty(response.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or Password is incorrect");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = response;
            return Ok(_response);
        }


        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registerDTO)
        {
            List<string> reservedUsernames = ["admin", "administration", "administrator", "system", "root"];

            if (reservedUsernames.Contains(registerDTO.Username.ToLower()))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Account with this Username or Email already exists.");
                return BadRequest(_response);
            }
            if (registerDTO.Username.Length < 6 || registerDTO.Username.Length > 20)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username should be between 6 and 20 characters");
                return BadRequest(_response);
            }

            // Check if password field equalls confirm password field
            var isPasswordConfirmed = registerDTO.Password == registerDTO.ConfirmPassword ? true : false;

            if (!isPasswordConfirmed)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Password and Confirm Password does not match");
                return BadRequest(_response);
            }

            // Check if username or email is used or not
            var isUnique = _authRepo.IsUniqueUser(registerDTO);
            if (!isUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Account with this Username or Email already exists.");
                return BadRequest(_response);
            }

            RegistrationResponseDTO user = await _authRepo.Register(registerDTO);
            return Ok(user);
        }
    }
}