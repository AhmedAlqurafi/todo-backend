using backend.Models.DTO.CategoryDTO;

namespace backend.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryDTO>> GetAllCategories();
        public Task<CategoryDTO> GetCategoryById(int Id);
        public Task CreateCategory(CategoryCreateDTO categoryCreateDTO);
        public Task UpdateCategory(int Id, CategoryUpdateDTO categoryUpdateDTO);
        public Task DeleteCategory(int Id);
    }
}