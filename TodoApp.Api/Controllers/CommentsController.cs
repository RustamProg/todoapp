using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly ITodoCommentsService _todoCommentsService;

        public CommentsController(ITodoCommentsService todoCommentsService)
        {
            _todoCommentsService = todoCommentsService;
        }

        [HttpGet("all-comments")]
        public IActionResult GetAllComments()
        {
            return Ok(_todoCommentsService.GetAllComments());
        }
        
        [HttpGet("todos-comments")]
        public IActionResult GetCommentsByTodo(long todoId)
        {
            return Ok(_todoCommentsService.GetCommentsByTodo(todoId));
        }

        [HttpGet("comment-by-id")]
        public IActionResult GetCommentById(long commentId)
        {
            return Ok(_todoCommentsService.GetCommentById(commentId));
        }
        
        [HttpPost("post-comment")]
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