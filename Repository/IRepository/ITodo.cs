using backend.Migrations.DTO;
using backend.Models.DTO.TodoDTO;

namespace backend.Repository.IRepository
{
    public interface ITodoRepository
    {
        public Task<List<TodoGetDTO>> GetAllTodos();
        public Task<List<TodoGetDTO>> GetMyTodos(int userId);
        public Task<List<TodoGetDTO>> GetUserTodos(int userId);
        public Task<TodoGetDTO> GetTodoById(int todoId);
        public Task<TodoGetDTO> CreateTodo(TodoCreateDTO todoCreateDTO, int userId);
        public Task UpdateTodo(TodoUpdateDTO todoDTO);
        public Task<bool> DeleteTodo(int Id);

        public Task<TodoGetDTO> UpdateStatusToInProgress(int Id);
        public Task<TodoGetDTO> UpdateStatusToCompleted(int Id);
        public Task<List<TodoGetDTO>> GetCompletedTodos();
        public Task<List<TodoGetDTO>> GetInProgresssTodos();
        /*  
  // Filtering and searching
    Task<IEnumerable<TodoDTO>> GetByUserIdAsync(int userId);
    Task<IEnumerable<TodoDTO>> GetByPriorityAsync(int priorityId);
    Task<IEnumerable<TodoDTO>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<TodoDTO>> SearchAsync(string searchTerm);
    

*/
    }
}