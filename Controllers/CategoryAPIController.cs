using System.Net;
using System.Runtime.CompilerServices;
using backend.Models;
using backend.Models.DTO.CategoryDTO;
using backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private APIResponse _response;
        private readonly ICategoryRepository _categoryRepo;
        public CategoryAPIController(ApplicationDbContext db, ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _db = db;
            this._response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int id)
        {

            var category = await _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Category not found");
                return NotFound(_response);
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            //Check if user exist or not
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == categoryCreateDTO.UserId);

            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Invalid user id");
                return NotFound(_response);
            }
            await _categoryRepo.CreateCategory(categoryCreateDTO);
            return Ok();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {

            var category = await _categoryRepo.UpdateCategory(id, categoryUpdateDTO);
            if (category == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Category not found");
                return NotFound(_response);
            }
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepo.DeleteCategory(id);
            return Ok();
        }
    }
}