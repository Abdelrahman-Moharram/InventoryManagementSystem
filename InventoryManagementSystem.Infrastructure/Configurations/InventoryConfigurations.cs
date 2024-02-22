using InventoryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InventoryManagementSystem.Infrastructure.Configurations
{
    public class InventoryConfigurations : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
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
