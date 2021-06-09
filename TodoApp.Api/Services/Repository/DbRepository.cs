using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models.DbContexts;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services.Repository
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

        public async Task AddAsyncRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Remove<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveRange<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return _context.Set<T>().Where(expression).AsQueryable();
        }

        public IQueryable<T> FindWithInclude<T, TP>(Expression<Func<T, bool>> expression, Expression<Func<T, TP>> navigationPropertyPath) where T : BaseEntity
        {
            return _context.Set<T>().Include(navigationPropertyPath).Where(expression).AsQueryable();
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetAllWithInclude<T, TP>(Expression<Func<T, TP>> navigationPropertyPath) where T : BaseEntity
        {
            return _context.Set<T>().Include(navigationPropertyPath).AsQueryable();
        }

        public T GetById<T>(long id) where T : BaseEntity
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }
    }
}