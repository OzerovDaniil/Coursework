using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiManagementSystem.Domain.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public required string ItemName { get; set; }
        public required string Unit { get; set; }

        public required Ingredient Ingredient { get; set; }
    }
}