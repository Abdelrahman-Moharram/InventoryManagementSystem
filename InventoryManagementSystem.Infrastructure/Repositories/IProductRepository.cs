using InventoryManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByName(string Name);
    }
}
