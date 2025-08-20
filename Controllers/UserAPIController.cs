using System.Security.Claims;
using backend.Models.DTO.UserDTO;
using backend.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogger<UserAPIController> _logger;
        ApplicationDbContext _db;
        public UserAPIController(IUserRepository userRepo, ILogger<UserAPIController> logger, ApplicationDbContext db)
        {
            _userRepo = userRepo;
            _db = db;
            _logger = logger;
        }

        [HttpGet("blah")]
        public string GetBlah()
        {
            return "bb";
        }


        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                // Extract user ID from JWT token claims
                var userIdClaim = User.Identity?.Name;
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token");
                }

                // Get user data from repository
                var user = await _userRepo.GetMe(Int32.Parse(userIdClaim));

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user information");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetUserById(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            UserGetDTO userGetDTO = await _userRepo.GetUserById(id);

            return Ok(userGetDTO);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO userUpdateDTO, int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await _userRepo.UpdateUser(id, userUpdateDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await _userRepo.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

    }
}