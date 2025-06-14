using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ідентифікатор замовлення обов'язковий")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Ідентифікатор страви обов'язковий")]
        public int MenuItemId { get; set; }

        [Required(ErrorMessage = "Кількість обов'язкова")]
        [Range(1, int.MaxValue, ErrorMessage = "Кількість повинна бути більшою за 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Ціна обов'язкова")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна повинна бути більшою за 0")]
        public decimal Price { get; set; }

        public string? SpecialInstructions { get; set; }
    }
}