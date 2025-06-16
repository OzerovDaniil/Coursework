using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Дата замовлення обов'язкова")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Статус обов'язковий")]
        [StringLength(50, ErrorMessage = "Статус не може перевищувати 50 символів")]
        public required string Status { get; set; }

        [Required(ErrorMessage = "UserId обов'язковий")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Загальна сума обов'язкова")]
        public int TotalPrice { get; set; }

        [Required(ErrorMessage = "Адреса доставки обов'язкова")]
        public required string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Метод оплати обов'язковий")]
        public required string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Дата доставки обов'язкова")]
        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "Статус доставки обов'язковий")]
        public required string DeliveryStatus { get; set; }

        public string? Comment { get; set; }
        public string? PromoCode { get; set; }

        [Required(ErrorMessage = "Ідентифікатор клієнта обов'язковий")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Ідентифікатор співробітника обов'язковий")]
        public int EmployeeId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}