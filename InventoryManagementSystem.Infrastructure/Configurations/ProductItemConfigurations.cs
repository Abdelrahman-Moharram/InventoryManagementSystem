using InventoryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            

            builder.HasKey(
                i => i.Id);


            builder
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder
                .HasOne(i => i.Inventory)
                .WithMany(ii => ii.ProductItems)
                .HasForeignKey(i => i.InventoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(i => i.Product)
                .WithMany(ii => ii.ProductItems)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(i => i.ProductsInventory)
                .WithMany(ii => ii.ProductItems)
                .HasForeignKey(i=>i.ProductsInventoryId)
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasOne(i => i.Order)
                .WithMany(ii=>ii.ProductItems)
                .HasForeignKey(i=>i.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasQueryFilter(i => !i.IsDeleted )
                .HasQueryFilter(ii=> !ii.IsSelled);

        }
    }
}
