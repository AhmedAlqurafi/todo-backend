using AutoMapper;
using backend.Models.DTO.CategoryDTO;
using backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var cats = await _db.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(cats);
        }

        public async Task<CategoryDTO> GetCategoryById(int Id)
        {
            var cat = await _db.Categories.FirstOrDefaultAsync(u => u.Id == Id);
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

        public async Task UpdateCategory(int Id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var cat = await _db.Categories.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Id);
            cat.CategoryName = categoryUpdateDTO.CategoryName;
            _db.Update(cat);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCategory(int Id)
        {
            var cat = await _db.Categories.FirstOrDefaultAsync(u => u.Id == Id);
            _db.Remove(cat);
            await _db.SaveChangesAsync();
    }
    }
}