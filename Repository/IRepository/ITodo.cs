using backend.Migrations.DTO;
using backend.Models.DTO.TodoDTO;

namespace backend.Repository.IRepository
{
    public interface ITodoRepository
    {
        public Task CreateTodo(TodoCreateDTO todoCreateDTO, int userId);
        public Task<List<TodoGetDTO>> GetAllTodo();
        public Task<TodoGetDTO> GetTodo(int Id);
        public Task UpdateTodo(TodoUpdateDTO todoDTO);
        public Task DeleteTodo(int Id);

        /*  
  // Filtering and searching
    Task<IEnumerable<TodoDTO>> GetByUserIdAsync(int userId);
    Task<IEnumerable<TodoDTO>> GetByStatusAsync(int statusId);
    Task<IEnumerable<TodoDTO>> GetByPriorityAsync(int priorityId);
    Task<IEnumerable<TodoDTO>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<TodoDTO>> SearchAsync(string searchTerm);
    

*/
    }
}