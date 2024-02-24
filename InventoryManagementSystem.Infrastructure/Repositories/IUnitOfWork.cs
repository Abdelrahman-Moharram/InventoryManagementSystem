using InventoryManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Inventory> Inventories { get; }
        IBaseRepository<Category> Categories { get; }
        IProductRepository Products { get; }
        IBaseRepository<ProductsInventory> ProductsInventories { get; }
        IBaseRepository<ProductItem> ProductItems { get; }
        IBaseRepository<Order> Orders { get; }
        IBaseRepository<Brand> Brands { get; }

        Task<int> Save();
    }
}
