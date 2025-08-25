namespace backend.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDTO> GetAllCategories();
        public Task<CategoryDTO> GetCategoryById(int )
    }
}