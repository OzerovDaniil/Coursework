using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Domain.Configurations
{
       public class UserConfiguration : IEntityTypeConfiguration<Employee>
       {
              public void Configure(EntityTypeBuilder<Employee> builder)
              {
                     builder.HasKey(u => u.Id);
                     builder.Property(u => u.Username)
                            .IsRequired()
                            .HasMaxLength(50);
                     builder.Property(u => u.PasswordHash)
                            .IsRequired();
                     builder.Property(u => u.Role)
                            .IsRequired()
                            .HasMaxLength(20);

                     // Зв'язок 1:М с Order
                     builder.HasMany(u => u.OrdersHandled)
                            .WithOne(o => o.Employee)
                            .HasForeignKey(o => o.EmployeeId)
                            .OnDelete(DeleteBehavior.Restrict);
              }
       }
}