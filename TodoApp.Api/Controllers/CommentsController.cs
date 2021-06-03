using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;
using TodoApp.Api.Services.ServicesAbstractions;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly ITodoCommentsService _todoCommentsService;

        public CommentsController(ITodoCommentsService todoCommentsService)
        {
            _todoCommentsService = todoCommentsService;
        }

        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllComments()
        {
            return Ok(_todoCommentsService.GetAllComments());
        }
        
        [HttpGet("todos")]
        public IActionResult GetCommentsByTodo(long todoId)
        {
            return Ok(_todoCommentsService.GetCommentsByTodo(todoId));
        }

        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(long commentId)
        {
            return Ok(_todoCommentsService.GetCommentById(commentId));
        }
        
        [HttpPost]
        public async Task<IActionResult> PostComment([FromForm]TodoCommentDto todoCommentDto)
        {
            if (todoCommentDto == null)
            {
                return BadRequest("Invalid comment parameters");
            }

            await _todoCommentsService.PostComment(todoCommentDto);
            return Ok();
        }
    }
}