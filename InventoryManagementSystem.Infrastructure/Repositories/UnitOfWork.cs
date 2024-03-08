using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Data;


namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Inventory> Inventories { get; private set;}
        public IBaseRepository<Category> Categories { get; private set;}
        public IProductRepository Products { get; private set;}
        public IBaseRepository<ProductsInventory> ProductsInventories { get; private set;}
        public IBaseRepository<ProductItem> ProductItems { get; private set;}
        public IBaseRepository<Order> Orders { get; private set;}
        public IBaseRepository<Brand> Brands { get; private set; }
        public IBaseRepository<UploadedFile> UploadedFiles { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context                = context;

            Inventories             = new BaseRepository<Inventory>(_context);
            Categories              = new BaseRepository<Category>(_context);
            Products                = new ProductRepository(_context);
            ProductsInventories     = new BaseRepository<ProductsInventory>(_context);
            ProductItems            = new BaseRepository<ProductItem>(_context);
            Orders                  = new BaseRepository<Order>(_context);
            Brands                  = new BaseRepository<Brand>(_context);
            UploadedFiles           = new BaseRepository<UploadedFile>(_context);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
