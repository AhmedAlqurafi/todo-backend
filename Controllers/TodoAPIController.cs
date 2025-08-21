using backend.Models.DTO.TodoDTO;
using backend.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoAPIController : ControllerBase
    {
        private readonly ITodoRepository _todoRepo;

        public TodoAPIController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string GetTodos()
        {
            return "bbb";
        }

        [HttpPost]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTodo(TodoCreateDTO todoCreateDTO)
        {


            // var userId = User.Identity?.Name;
            // if (userId == null)
            // {
            //     return Unauthorized("User Id not found");
            // }

            await _todoRepo.CreateTodo(todoCreateDTO, Int32.Parse("1"));


            return Created();
        }
    }
}