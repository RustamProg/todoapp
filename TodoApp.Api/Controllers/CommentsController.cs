using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services;
using TodoApp.Api.Services.ServicesAbstractions;

namespace TodoApp.Api.Controllers
{
    /// <summary>
    /// Комментарии
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly ITodoCommentsService _todoCommentsService;

        /// <summary>
        /// Контроллер управления комментариями
        /// </summary>
        /// <param name="todoCommentsService"></param>
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
        
        /// <summary>
        /// Получение всех комментариев у определенного задания (Task)
        /// </summary>
        /// <param name="todoId">Идентификатор задания</param>
        /// <returns></returns>
        [HttpGet("todos")]
        public IActionResult GetCommentsByTodo(long todoId)
        {
            return Ok(_todoCommentsService.GetCommentsByTodo(todoId));
        }

        /// <summary>
        /// Получение комментария по ID
        /// </summary>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(long commentId)
        {
            return Ok(_todoCommentsService.GetCommentById(commentId));
        }
        
        /// <summary>
        /// Создать комментарий
        /// </summary>
        /// <param name="todoCommentDto"></param>
        /// <returns></returns>
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