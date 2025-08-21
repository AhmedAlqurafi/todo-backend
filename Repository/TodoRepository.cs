using AutoMapper;
using backend.Migrations.DTO;
using backend.Models.DTO.TodoDTO;
using backend.Repository.IRepository;

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

        public async Task CreateTodo(TodoCreateDTO todo, int userId)
        {
            Todo newTodo = new()
            {
                UserId = userId,
                Title = todo.Title,
                Details = todo.Details,
                PriorityId = 1,
                StatusId = 1,
                CategoryId = 1,
                ImageURL = "bbb",
                Deadline = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            // Todo todo = _mapper.Map<Todo>(todoCreateDTO);
            // Todo todo = new()
            // {
            //     Id = 1,
            //     UserId = userId,
            //     Title = todoCreateDTO.Title,
            //     Details = todoCreateDTO.Details,
            //     PriorityId = 1,
            //     StatusId = 1,
            //     CategoryId = 1,
            //     ImageURL = "bbb",
            //     Deadline = DateTime.Now,
            //     CreatedAt = DateTime.Now,
            //     UpdatedAt = DateTime.Now

            // };
            await _db.Todos.AddAsync(newTodo);
            await _db.SaveChangesAsync();
        }

        public Task DeleteTodo(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoGetDTO>> GetAllTodo()
        {
            throw new NotImplementedException();
        }

        public Task<TodoGetDTO> GetTodo(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodo(TodoUpdateDTO todoDTO)
        {
            throw new NotImplementedException();
        }
    }
}