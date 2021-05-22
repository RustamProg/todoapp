using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;

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

        [HttpGet("get-todos")]
        public IActionResult GetTodos()
        {
            var todos = _todoService.GetAllTodos();
            return Ok(todos);
        }

        [HttpPost("create-todo")]
        public async Task<IActionResult> CreateTodo(TodoDto todo)
        {
            if (todo == null)
            {
                return BadRequest("Invalid input parameters");
            }
            
            var result = await _todoService.CreateNewTodo(todo);
            return Created("http://localhost:5000/", result);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoById(long id)
        {
            return Ok(_todoService.GetTodoById(id));
        }

        [HttpGet("current-user-todos")]
        public IActionResult GetUser()
        {
            return Ok(_todoService.GetUsersTodos());
        }
    }
}