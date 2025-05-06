using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public required string DeliveryAddress { get; set; }
        public required string PaymentMethod { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public string? Comment { get; set; }
        public string? PromoCode { get; set; }

        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public required Employee Employee { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}