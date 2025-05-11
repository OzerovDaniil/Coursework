using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiManagementSystem.Domain.Entities;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Username).IsRequired().HasMaxLength(50);
        builder.Property(c => c.PasswordHash).IsRequired();
        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15);

        builder.Property(c => c.Surname).HasMaxLength(100);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.Address).HasMaxLength(200);

        // Связь 1:M с Order
        builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}