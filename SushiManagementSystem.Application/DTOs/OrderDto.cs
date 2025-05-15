using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SushiManagementSystem.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Дата замовлення обов'язкова")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Статус обов'язковий")]
        [StringLength(50, ErrorMessage = "Статус не може перевищувати 50 символів")]
        public required string Status { get; set; }

        [Required(ErrorMessage = "Ідентифікатор клієнта обов'язковий")]
        public int CustomerId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Загальна сума повинна бути невід'ємною")]
        public decimal TotalPrice { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}