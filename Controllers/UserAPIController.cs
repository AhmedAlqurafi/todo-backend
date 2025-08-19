using backend.Models.DTO.UserDTO;
using backend.Repository.IRepository;
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
        [HttpGet("{id:int}", Name="GetUserById")]
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
    }
}