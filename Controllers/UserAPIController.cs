using backend.Models.DTO.UserDTO;
using backend.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        ApplicationDbContext _db;
        public UserAPIController(IUserRepository userRepo, ApplicationDbContext db)
        {
            _userRepo = userRepo;
            _db = db;
        }

        [HttpGet("blah")]
        public string GetBlah()
        {
            return "bb";
        }


        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMe()
        {
            // _userRepo.GetMe(jwtToken);
            return Ok("BBB");
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