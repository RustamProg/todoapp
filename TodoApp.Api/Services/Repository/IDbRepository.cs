using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services.Repository
{
    public interface IDbRepository
    {
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task AddAsyncRange<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task<T> Remove<T>(T entity) where T : BaseEntity;
        Task RemoveRange<T>(IEnumerable<T> entities) where T : BaseEntity;
        IQueryable<T> Find<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        IQueryable<T> FindWithInclude<T, TP>(Expression<Func<T, bool>> expression, Expression<Func<T, TP>> navigationPropertyPath) where T : BaseEntity; //
        IQueryable<T> GetAll<T>() where T : BaseEntity;
        IQueryable<T> GetAllWithInclude<T, TP>(Expression<Func<T, TP>> navigationPropertyPath) where T : BaseEntity; //
        T GetById<T>(long id) where T : BaseEntity;
        
    }
}