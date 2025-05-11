using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();

        // Связь M:1 с Order
        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        // Связь M:1 с MenuItem
        builder.HasOne(oi => oi.MenuItem)
               .WithMany(m => m.OrderItems)
               .HasForeignKey(oi => oi.MenuItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}