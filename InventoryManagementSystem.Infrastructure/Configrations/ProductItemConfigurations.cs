using InventoryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Configrations
{
    public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {

            builder.HasKey(
                i => new { i.ProductId, i.InventoryId });

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
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasOne(i => i.Order)
                .WithMany(ii=>ii.ProductItems)
                .HasForeignKey(i=>i.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
