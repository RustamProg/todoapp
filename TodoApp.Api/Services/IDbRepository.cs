using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services
{
    public interface IDbRepository
    {
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        IQueryable<T> GetAll<T>() where T : BaseEntity;
        T GetById<T>(long id) where T : BaseEntity;
    }
}