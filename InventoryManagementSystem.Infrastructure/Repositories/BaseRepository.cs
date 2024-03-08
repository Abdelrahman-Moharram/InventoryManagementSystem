using InventoryManagementSystem.Domain.Constants;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private ApplicationDbContext _context { get; }

        private IQueryable<T> HandleIncludes(IQueryable<T> query, string[] includes = null, bool IgnoreGlobalFilters = false)
        {
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include).AsNoTracking();
                    if(IgnoreGlobalFilters)
                        query = query.IgnoreQueryFilters();


            return query;
        }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null, bool IgnoreGlobalFilters = false)
        {
            return await HandleIncludes(_context.Set<T>(), includes, IgnoreGlobalFilters).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Find(Expression<Func<T, bool>> expression, string[] includes = null, bool IgnoreGlobalFilters = false)
        {

            return await HandleIncludes(_context.Set<T>(), includes, IgnoreGlobalFilters).SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> expression, 
            int? take=null, 
            int? skip=null, 
            Expression<Func<T, object>> orderBy=null,
            string orderDirection = null,
            string[] includes = null,
            bool IgnoreGlobalFilters = false
            )
        {
            var query = HandleIncludes(_context.Set<T>(), includes, IgnoreGlobalFilters).Where(expression);


            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if(orderBy != null)
            {
                if (orderDirection == OrderDirections.Descending)
                    query.OrderByDescending(orderBy);
                else 
                    query.OrderBy(orderBy);
            }
            return await 
                query
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }


        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await Task.Run(() => _context.Set<T>().Update(entity));
            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().CountAsync(expression);
        }
    }
}
