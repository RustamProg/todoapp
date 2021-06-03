using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("todo")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly ICurrentUser _currentUser;

        public TodoController(ITodoService todoService, ICurrentUser currentUser)
        {
            _todoService = todoService;
            _currentUser = currentUser;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var todos = _todoService.GetAllTodos();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoDto todo)
        {
            if (todo == null)
            {
                return BadRequest("Invalid input parameters");
            }
            
            var result = await _todoService.CreateNewTodo(todo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoById(long id)
        {
            return Ok(_todoService.GetTodoById(id));
        }

        [HttpGet("current-user")]
        public ICurrentUser GetCurrentUser()
        {
            return _currentUser;
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(_todoService.GetUsersTodos());
        }
    }
}