using System;
using System.Collections.Generic;

namespace TodoApp.Api.Models.DbEntities
{
    public class Todo: BaseEntity
    {
        public string Title { get; set; }
        public string TextBody { get; set; }
        public ImportanceLevels TodoImportance { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime ExpirationDateTime { get; set; } = DateTime.Now.Add(TimeSpan.FromDays(7));
        public Guid AuthorId { get; set; }
        public string AuthorUsername { get; set; }
    }

    public enum ImportanceLevels
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh
    }
}