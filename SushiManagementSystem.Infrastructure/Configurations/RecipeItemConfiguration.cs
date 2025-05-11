using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Domain.Configurations
{
    public class RecipeItemConfiguration : IEntityTypeConfiguration<RecipeItem>
    {
        public void Configure(EntityTypeBuilder<RecipeItem> builder)
        {
            builder.HasKey(ri => ri.Id);

            builder.Property(ri => ri.Quantity).IsRequired();

            // Зв'язок M:1 з MenuItem
            builder.HasOne(ri => ri.MenuItem)
                   .WithMany(m => m.RecipeItems)
                   .HasForeignKey(ri => ri.MenuItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Зв'язок M:1 з Ingredient
            builder.HasOne(ri => ri.Ingredient)
                   .WithMany(i => i.RecipeItems)
                   .HasForeignKey(ri => ri.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Унікальний індекс для комбінації MenuItemId і IngredientId
            builder.HasIndex(ri => new { ri.MenuItemId, ri.IngredientId }).IsUnique();
        }
    }
}