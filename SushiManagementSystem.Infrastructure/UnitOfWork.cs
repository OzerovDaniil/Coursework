using SushiManagementSystem.Infrastructure.Data;
using SushiManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;
using SushiManagementSystem.Application.Interfaces;

namespace SushiManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Employees = new Repository<Employee>(context);
            Customers = new Repository<Customer>(context);
            MenuItems = new Repository<MenuItem>(context);
            Orders = new Repository<Order>(context);
            OrderItems = new Repository<OrderItem>(context);
            Ingredients = new Repository<Ingredient>(context);
            RecipeItems = new Repository<RecipeItem>(context);
            Inventories = new Repository<Inventory>(context);
        }

        public IRepository<Employee> Employees { get; }
        public IRepository<Customer> Customers { get; }
        public IRepository<MenuItem> MenuItems { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<OrderItem> OrderItems { get; }
        public IRepository<Ingredient> Ingredients { get; }
        public IRepository<RecipeItem> RecipeItems { get; }
        public IRepository<Inventory> Inventories { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}