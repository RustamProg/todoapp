using System;
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
        
        // todo: add user 
        private readonly IDbRepository _dbRepository;
        private readonly SqlServerDbContext _context;

        public TodoService(IDbRepository dbRepository, SqlServerDbContext context)
        {
            _dbRepository = dbRepository;
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
                AuthorId = Guid.NewGuid(),
                AuthorUsername = "not impl"
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