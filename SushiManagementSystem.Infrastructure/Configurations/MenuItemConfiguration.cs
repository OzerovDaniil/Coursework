using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Domain.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Price).IsRequired();

            // Зв'язок 1:M з OrderItem
            builder.HasMany(m => m.OrderItems)
                   .WithOne(oi => oi.MenuItem)
                   .HasForeignKey(oi => oi.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Зв'язок 1:M з RecipeItem для зв'язку M:M з Ingredients
            builder.HasMany(m => m.RecipeItems)
                   .WithOne(ri => ri.MenuItem)
                   .HasForeignKey(ri => ri.MenuItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.Name);
        }
    }
}