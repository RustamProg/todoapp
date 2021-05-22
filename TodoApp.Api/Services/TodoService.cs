using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbContexts;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services
{
    public class TodoService: ITodoService
    {
        private readonly IDbRepository _dbRepository;
        private readonly ICurrentUser _currentUser;
        private readonly SqlServerDbContext _context;

        public TodoService(IDbRepository dbRepository, ICurrentUser currentUser, SqlServerDbContext context)
        {
            _dbRepository = dbRepository;
            _currentUser = currentUser;
            _context = context;
        }

        public async Task<Todo> CreateNewTodo(TodoDto newTodo)
        {
            var todo = new Todo
            {
                Title = newTodo.Title,
                TextBody = newTodo.TextBody,
                ExpirationDateTime = newTodo.ExpirationDateTime,
                TodoImportance = newTodo.TodoImportance,
                AuthorId = _currentUser.Id,
                AuthorUsername = _currentUser.Username
            };

            await _dbRepository.AddAsync(todo);
            return todo;
        }

        public Todo GetTodoById(long id)
        {
            return _dbRepository.GetById<Todo>(id);
        }

        public List<Todo> GetAllTodos()
        {
            return _dbRepository.GetAll<Todo>().ToList();
        }

        
        // Здесь напрямую контекст использовал (надо исправить)
        public List<Todo> GetUsersTodos()
        {
            return _context.Todos.Where(x => x.AuthorId == _currentUser.Id).ToList();
        }
    }
}