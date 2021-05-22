using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.Models.DbContexts;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services
{
    public class DbRepository: IDbRepository
    {
        private readonly SqlServerDbContext _context;

        public DbRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public T GetById<T>(long id) where T : BaseEntity
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }
    }
}