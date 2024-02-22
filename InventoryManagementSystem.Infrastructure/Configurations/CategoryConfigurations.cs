using InventoryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Infrastructure.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GetDate()");
        }
    }
}
