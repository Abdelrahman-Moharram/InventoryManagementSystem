using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context) { }


        public async  Task<Product> GetByName(string Name)
        {
            return await  _context.Products.FirstOrDefaultAsync(i=>i.Name == Name);
        }
    }
}
