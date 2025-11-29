using AutoMapper;
using backend.Models.DTO.TodoDTO;
using backend.Models.StatisticsDTO;
using backend.Repository.IRepository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> DeleteTodo(int Id)
        {
            var todo = await _db.Todos.FirstOrDefaultAsync(todo => todo.Id == Id);

            if (todo == null)
            {
                return false;
            }

            _db.Remove(todo);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TodoGetDTO> UpdateStatusToInProgress(int Id)
        {
            var todo = await _db.Todos.AsNoTracking().FirstOrDefaultAsync(todo => todo.Id == Id);

            if (todo == null)
            {
                return null!;
            }

            Todo updatedTodo = new()
            {
                Id = todo.Id,
                UserId = todo.UserId,
                Title = todo.Title,
                Details = todo.Details,
                PriorityId = todo.PriorityId,
                StatusId = 2, // Updated 
                CategoryId = todo.CategoryId,
                ImageURL = todo.ImageURL,
                Deadline = todo.Deadline,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = DateTime.Now
            };
            _db.Update(updatedTodo);
            await _db.SaveChangesAsync();
            return _mapper.Map<TodoGetDTO>(updatedTodo);
        }

        public async Task<TodoGetDTO> UpdateStatusToCompleted(int Id)
        {

            var todo = await _db.Todos.AsNoTracking().FirstOrDefaultAsync(todo => todo.Id == Id);

            if (todo == null)
            {
                return null!;
            }

            Todo updatedTodo = new()
            {
                Id = todo.Id,
                UserId = todo.UserId,
                Title = todo.Title,
                Details = todo.Details,
                PriorityId = todo.PriorityId,
                StatusId = 3, // Updated 
                CategoryId = todo.CategoryId,
                ImageURL = todo.ImageURL,
                Deadline = todo.Deadline,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = DateTime.Now
            };
            _db.Update(updatedTodo);
            await _db.SaveChangesAsync();
            return _mapper.Map<TodoGetDTO>(updatedTodo);
        }

        public async Task<List<TodoGetDTO>> GetCompletedTodos(int UserId)
        {
            var todos = await _db.Todos.Where(todos => todos.UserId == UserId && todos.StatusId == 3).ToListAsync();
            return _mapper.Map<List<TodoGetDTO>>(todos);
        }

        public async Task<List<TodoGetDTO>> GetInProgresssTodos(int UserId)
        {
            var todos = await _db.Todos.Where(todos => todos.UserId == UserId && todos.StatusId == 2).ToListAsync();
            return _mapper.Map<List<TodoGetDTO>>(todos);
        }

        public async Task<StatisticsDTO[]> GetTodoStatistics(int UserId)
        {
            var todos = await _db.Todos.Where(todo => todo.UserId == UserId).ToListAsync();

            var notStarted = 0;
            var inProgress = 0;
            var completed = 0;
            var totalTodos = todos.Count();

            foreach (var todo in todos)
            {
                switch (todo.StatusId)
                {
                    case 1:
                        notStarted++;
                        break;

                    case 2:
                        inProgress++;
                        break;

                    case 3:
                        completed++;
                        break;
                }
            }

            var statistics = new StatisticsDTO[]
            {
                new StatisticsDTO
                {
                   type = "Not Started",
                   percentage = (int)Math.Round((float)notStarted / totalTodos * 100)
                },
                new StatisticsDTO
                {
                   type = "In-Progress",
                   percentage = (int)Math.Round((float)inProgress/ totalTodos * 100)
                },
                new StatisticsDTO
                {
                   type = "Completed",
                   percentage = (int)Math.Round((float)completed/ totalTodos * 100)
                },
            };

            return statistics;
        }
    }
}