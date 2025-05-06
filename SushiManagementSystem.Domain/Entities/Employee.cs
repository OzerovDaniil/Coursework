using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; } // Наприклад, "Admin", "Waiter", "Chef"
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public ICollection<Order> OrdersHandled { get; set; } = new List<Order>();
    }
}