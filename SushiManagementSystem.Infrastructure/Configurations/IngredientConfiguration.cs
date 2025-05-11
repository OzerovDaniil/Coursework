using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Domain.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).IsRequired().HasMaxLength(50);
            builder.Property(i => i.PricePerUnit).IsRequired();

            // Зв'язок 1:1 з Inventory
            builder.HasOne(i => i.Inventory)
                   .WithOne(inv => inv.Ingredient)
                   .HasForeignKey<Inventory>(inv => inv.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Зв'язок 1:M з RecipeItem для зв'язку M:M з MenuItems
            builder.HasMany(i => i.RecipeItems)
                   .WithOne(ri => ri.Ingredient)
                   .HasForeignKey(ri => ri.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}