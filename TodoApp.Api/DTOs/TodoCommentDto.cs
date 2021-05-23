using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.DTOs
{
    public class TodoCommentDto
    {
        public string CommentBody { get; set; }
        public long ParentTodoId { get; set; }
    }
}