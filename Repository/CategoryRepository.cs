using AutoMapper;
using backend.Models;
using backend.Models.DTO.CategoryDTO;
using backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private APIResponse _response;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            this._response = new APIResponse();
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var cats = await _db.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(cats);
        }

        public async Task<CategoryDTO?> GetCategoryById(int Id)
        {

            var cat = await _db.Categories.FirstOrDefaultAsync(u => u.Id == Id);

            if (cat == null)
            {
                return null;
            }

            CategoryDTO catDTO = _mapper.Map<CategoryDTO>(cat);
            return catDTO;
        }

        public async Task CreateCategory(CategoryCreateDTO categoryDTO)
        {

            Category cat = new()
            {
                CategoryName = categoryDTO.CategoryName,
                UserId = categoryDTO.UserId,
                CreatedAt = DateTime.Now,
            };

            await _db.Categories.AddAsync(cat);
            await _db.SaveChangesAsync();
        }

        public bool IsCategoryHasTodo(int categoryId)
        {
            var catNum = _db.Todos.Count(t => t.CategoryId == categoryId);
            if (catNum > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<CategoryDTO> UpdateCategory(int Id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var cat = await _db.Categories.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Id);

            if (cat == null)
            {
                return null;
            }

            cat.CategoryName = categoryUpdateDTO.CategoryName;
            cat.UpdatedAt = DateTime.Now;
            _db.Update(cat);
            await _db.SaveChangesAsync();
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(cat);
            return categoryDTO;
        }

        public async Task DeleteCategory(int Id)
        {
            //TODO: Delete the category and todos linked to it.
            var cat = await _db.Categories.FirstOrDefaultAsync(u => u.Id == Id);
            _db.Remove(cat);
            await _db.SaveChangesAsync();
        }
    }
}