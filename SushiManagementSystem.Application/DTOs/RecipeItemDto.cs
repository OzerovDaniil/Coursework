using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class RecipeItemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ідентифікатор страви обов'язковий")]
        public int MenuItemId { get; set; }

        [Required(ErrorMessage = "Ідентифікатор інгредієнта обов'язковий")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Кількість обов'язкова")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Кількість повинна бути більшою за 0")]
        public decimal Quantity { get; set; }
    }
}