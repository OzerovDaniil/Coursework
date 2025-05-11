using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Address { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}