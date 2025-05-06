using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public string? Unit { get; set; } // Наприклад, "г", "кг", "шт" тощо
        public bool IsAvailable { get; set; } = true;
        public string? Category { get; set; } // Наприклад, "Риба", "Овочі", "Соуси" тощо


        public ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
        public required Inventory Inventory { get; set; }
    }
}