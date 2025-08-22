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
        // TODO: Implement this
        // [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoRepo.GetAllTodos();
            return Ok(todos);
        }
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyTodos()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }
            var todos = await _todoRepo.GetMyTodos(Int32.Parse(userId));
            return Ok(todos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodoById(int todoId)
        {
            var todo = await _todoRepo.GetTodoById(todoId);
            return Ok(todo);
        }

        [HttpGet("user/{id:int}")]
        // TODO: Implement this
        // [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserTodos(int id)
        {
            var todos = await _todoRepo.GetUserTodos(id);
            return Ok(todos);
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