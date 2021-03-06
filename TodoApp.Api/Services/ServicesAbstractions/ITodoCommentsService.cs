using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services.ServicesAbstractions
{
    public interface ITodoCommentsService
    {
        TodoComment GetCommentById(long id);
        List<TodoComment> GetCommentsByTodo(long todoId);
        List<TodoComment> GetAllComments();
        Task<TodoComment> PostComment(TodoCommentDto todoCommentDto);
        Task<TodoComment> DeleteComment(long commentId);
    }
}