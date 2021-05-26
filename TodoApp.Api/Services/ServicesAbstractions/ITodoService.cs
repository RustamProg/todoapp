using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services.ServicesAbstractions
{
    public interface ITodoService
    {
        Task<Todo> CreateNewTodo(TodoDto newTodo);
        Todo GetTodoById(long id);
        List<Todo> GetAllTodos();
        List<Todo> GetUsersTodos();
    }
}