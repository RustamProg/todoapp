using System;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.DTOs
{
    public class TodoDto
    {
        public string Title { get; set; }
        public string TextBody { get; set; }
        public ImportanceLevels TodoImportance { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public long ProjectId { get; set; }
    }
}