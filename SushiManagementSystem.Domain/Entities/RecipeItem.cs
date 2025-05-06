using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class RecipeItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public required string Unit { get; set; } // Наприклад, "г", "кг", "шт" тощо
        public string? SpecialInstructions { get; set; } // Додаткові інструкції для приготування


        public required MenuItem MenuItem { get; set; }
        public required Ingredient Ingredient { get; set; }
    }
}