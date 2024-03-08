using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity Tabels
            builder.Entity<ApplicationUser>().ToTable("Users", schema: "Identity");
            builder.Entity<IdentityRole>().ToTable("Roles", schema: "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", schema: "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", schema: "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", schema: "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", schema: "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", schema: "Identity");

            // Models Configurations

            new ApplicationUserConfigurations().Configure(builder.Entity<ApplicationUser>());

            new CustomerConfigurations().Configure(builder.Entity<Customer>());
            new SupplierConfigurations().Configure(builder.Entity<Supplier>());
            new InventoryConfigurations().Configure(builder.Entity<Inventory>());
            new CategoryConfigurations().Configure(builder.Entity<Category>());
            new ProductConfigurations().Configure(builder.Entity<Product>());
            
            // ProductsInventoryConfigurations -> included in ProductConfigurations 

            new ProductItemConfigurations().Configure(builder.Entity<ProductItem>());
            new OrderConfigurations().Configure(builder.Entity<Order>());
            new BrandsConfigurations().Configure(builder.Entity<Brand>());

            builder.Entity<UploadedFile>()
                .HasOne(i => i.Product)
                .WithMany(i => i.UploadedFiles)
                .HasForeignKey(i => i.ProductId);

            builder.Entity<UploadedFile>()
                .HasQueryFilter(i=>!i.IsDeleted);

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Inventory> Inventories { get; set; }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsInventory> ProductsInventories { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }


    }
}
