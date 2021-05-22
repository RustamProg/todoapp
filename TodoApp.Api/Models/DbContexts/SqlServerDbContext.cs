using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Models.DbContexts
{
    public class SqlServerDbContext: DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options): base(options)
        {
            
        }
        
        public DbSet<Todo> Todos { get; set; }
    }
}