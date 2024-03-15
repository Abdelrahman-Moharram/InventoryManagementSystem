using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null, bool IgnoreGlobalFilters = false);
        Task<T> GetByIdAsync(string id);
        Task<T> Find(Expression<Func<T, bool>> expression, string[] includes = null, bool IgnoreGlobalFilters = false);

        Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> expression,
            int? take=null,
            int? skip= null,
            Expression<Func<T, object>> orderBy = null,
            string orderDirection = null,
            string[] includes = null, 
            bool IgnoreGlobalFilters = false
            );

        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<ICollection<TType>> GetAsSelectList<TType>(Expression<Func<T, TType>> select) where TType : class;
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
