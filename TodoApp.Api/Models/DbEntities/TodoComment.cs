using System;

namespace TodoApp.Api.Models.DbEntities
{
    public class TodoComment: BaseEntity
    {
        public string CommentBody { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        
        public long ParentTodoId { get; set; }
        public Todo ParentTodo { get; set; }
    }
}