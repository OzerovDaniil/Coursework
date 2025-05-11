using SushiManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> Employees { get; }
        IRepository<Customer> Customers { get; }
        IRepository<MenuItem> MenuItems { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Ingredient> Ingredients { get; }
        IRepository<RecipeItem> RecipeItems { get; }
        IRepository<Inventory> Inventories { get; }

        Task<int> SaveChangesAsync();
    }
}