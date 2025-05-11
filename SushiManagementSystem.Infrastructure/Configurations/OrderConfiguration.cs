using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Domain.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate)
                   .IsRequired();
            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            // Связь 1:М с User
            builder.HasOne(o => o.Employee)
                   .WithMany(u => u.OrdersHandled)
                   .HasForeignKey(o => o.EmployeeId);

            // Связь 1:М с Customer
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);
        }
    }
}