using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class IngredientDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва інгредієнта обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Ціна за одиницю обов'язкова")]
        public decimal PricePerUnit { get; set; }

        public string? Unit { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Category { get; set; }
    }
}
