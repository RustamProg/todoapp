using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;
using TodoApp.Api.Services.Repository;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Services.ServicesImplementations
{
    public class TodoService: ITodoService
    {
        
        // todo: add user 
        private readonly IDbRepository _dbRepository;
        private readonly ICurrentUser _currentUser;

        public TodoService(IDbRepository dbRepository, ICurrentUser currentUser)
        {
            _dbRepository = dbRepository;
            _currentUser = currentUser;
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
        /*public List<Todo> GetUsersTodos()
        {
            return _context.Todos.Where(x => x.AuthorId == _currentUser.Id).ToList();
        }*/
    }
}