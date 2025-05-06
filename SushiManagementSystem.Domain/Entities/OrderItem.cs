using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiManagementSystem.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [NotMapped] // Це поле не буде зберігатися в базі даних
        public decimal TotalPrice => Quantity * Price; // Загальна ціна для цього пункту замовлення
        public string? SpecialInstructions { get; set; } // Додаткові інструкції для приготування

        public required Order Order { get; set; }
        public required MenuItem MenuItem { get; set; }
    }
}