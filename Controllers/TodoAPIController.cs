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
        public async Task<IActionResult> GetTodoById(int id)
        {
            // TODO: Prevent getting todo from other users
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }

            var todo = await _todoRepo.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTodo(TodoCreateDTO todoCreateDTO)
        {
            try
            {
                var userIdClaim = User.Identity?.Name;
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                TodoGetDTO todo = await _todoRepo.CreateTodo(todoCreateDTO, Int32.Parse(userIdClaim));
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoUpdateDTO updateTodo, int id)
        {
            var userIdClaim = User.Identity?.Name;
            if (userIdClaim == null)
            {
                return Unauthorized(); ;
            }


            return Ok();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var userIdClaim = User.Identity?.Name;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var result = await _todoRepo.DeleteTodo(id);
            if (result == false)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpPost("{id:int}/update-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusToInProgress(int id)
        {

            //TODO: Add validation on the token, such as: Expiration and token belonging to the user
            var res = await _todoRepo.UpdateStatusToInProgress(id);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("{id:int}/completed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusToCompleted(int id)
        {

            //TODO: Add validation on the token, such as: Expiration and token belonging to the user
            var res = await _todoRepo.UpdateStatusToCompleted(id);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet("getInProgressTodos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetInProgressTodos()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }
            var todos = await _todoRepo.GetInProgresssTodos(Int32.Parse(userId));
            return Ok(todos);
        }

        [HttpGet("getCompletedTodos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCompletedTodos()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }
            var todos = await _todoRepo.GetCompletedTodos(Int32.Parse(userId));
            return Ok(todos);
        }


        [HttpGet("getStatistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetStatistics()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }

            var statistics = await _todoRepo.GetTodoStatistics(Int32.Parse(userId));
            return Ok(statistics);
        }

    }
}