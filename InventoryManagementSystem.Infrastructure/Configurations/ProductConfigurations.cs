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
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.Price)
                .HasDefaultValue(0)
                .HasColumnType("money");

            builder.HasOne(i => i.Category)
                .WithMany(ii=>ii.Products)
                .HasForeignKey(i => i.CategoryId);




            builder
                .HasMany(i => i.Inventories)
                .WithMany(ii => ii.Products)
                .UsingEntity<ProductsInventory>(
                    j=>
                    {
                        j
                        .HasOne(pi => pi.Product)
                        .WithMany(p => p.ProductsInventory)
                        .HasForeignKey(pi => pi.ProductId);

                        j
                        .HasOne(pi => pi.Inventory)
                        .WithMany(i => i.ProductsInventory)
                        .HasForeignKey(pi => pi.InventoryId);

                        j
                        .HasKey(j => new { j.ProductId, j.InventoryId });

                        j
                        .Property(i => i.Amount)
                        .HasDefaultValue(0);

                        j
                        .Property(i => i.CreatedAt)
                        .HasDefaultValueSql("GetDate()");

                    }
                );

        }
    }
}
