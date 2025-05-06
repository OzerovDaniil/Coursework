using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public required string Category { get; set; } // Наприклад, "Суші", "Роли", "Напої" тощо
        public bool IsAvailable { get; set; } = true;

        // Связь М:М с заказами через OrderItems и с ингредиентами через RecipeItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
    }
}
