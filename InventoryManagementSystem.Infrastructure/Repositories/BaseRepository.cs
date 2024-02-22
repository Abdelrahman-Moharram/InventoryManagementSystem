using InventoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ApplicationDbContext _context { get; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

    }
}
