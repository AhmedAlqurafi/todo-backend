using AutoMapper;
using backend.Migrations.DTO;
using backend.Models.DTO.TodoDTO;
using backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace backend.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public TodoRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<TodoGetDTO>> GetAllTodos()
        {
            var todos = await _db.Todos.ToListAsync();

            return _mapper.Map<List<TodoGetDTO>>(todos);
        }


        public async Task<List<TodoGetDTO>> GetMyTodos(int userId)
        {
            var todos = await _db.Todos.Where(todo => todo.UserId == userId).ToListAsync();
            return _mapper.Map<List<TodoGetDTO>>(todos);
        }

        public async Task<TodoGetDTO> GetTodoById(int Id)
        {
            var todo = await _db.Todos.FirstOrDefaultAsync(todo => todo.Id == Id);

            if (todo == null)
            {
                return null!;
            }

            return _mapper.Map<TodoGetDTO>(todo);
        }

        public async Task<List<TodoGetDTO>> GetUserTodos(int userId)
        {
            var todos = await _db.Todos.Where(todo => todo.UserId == userId).ToListAsync();
            return _mapper.Map<List<TodoGetDTO>>(todos);
        }

        public async Task<TodoGetDTO> CreateTodo(TodoCreateDTO todo, int userId)
        {

            Todo newTodo = new()
            {
                UserId = userId,
                Title = todo.Title,
                Details = todo.Details,
                PriorityId = todo.PriorityId,
                StatusId = 1,
                CategoryId = todo.CategoryId,
                ImageURL = todo.ImageURL,
                Deadline = todo.Deadline,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _db.Todos.AddAsync(newTodo);
            await _db.SaveChangesAsync();

            return _mapper.Map<TodoGetDTO>(newTodo);

        }
        public Task UpdateTodo(TodoUpdateDTO todoDTO)
        {
            throw new NotImplementedException();
        }
        public Task DeleteTodo(int Id)
        {
            throw new NotImplementedException();
        }


    }
}