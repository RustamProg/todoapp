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
    /// <summary>
    /// Задания (todos)
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("todo")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        /// <summary>
        /// Контроллер управления заданиями
        /// </summary>
        /// <param name="todoService"></param>
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Получить список всех заданий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTodos()
        {
            var todos = _todoService.GetAllTodos();
            return Ok(todos);
        }

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получить задание по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetTodoById(long id)
        {
            return Ok(_todoService.GetTodoById(id));
        }

        /// <summary>
        /// Получить задания ткущего пользователя (пользователь в токене)
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(_todoService.GetUsersTodos());
        }

        /// <summary>
        /// Удалить задание по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            return Ok(await _todoService.DeleteTodo(id));
        }
    }
}