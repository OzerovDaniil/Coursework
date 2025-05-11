using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Domain.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(inv => inv.Id);

            builder.Property(inv => inv.Quantity).IsRequired();

            // Зв'язок 1:1 з Ingredient
            builder.HasOne(inv => inv.Ingredient)
                   .WithOne(i => i.Inventory)
                   .HasForeignKey<Inventory>(inv => inv.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}