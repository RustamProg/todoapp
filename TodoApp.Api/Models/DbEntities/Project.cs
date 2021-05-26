using System;
using System.Collections.Generic;

namespace TodoApp.Api.Models.DbEntities
{
    public class Project: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public List<Todo> ChildrenTodos { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorUsername { get; set; }
    }
}