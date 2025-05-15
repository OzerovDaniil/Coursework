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

        [Required(ErrorMessage = "Кількість обов'язкова")]
        [Range(0, double.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public required decimal Quantity { get; set; }

        [Required(ErrorMessage = "Одиниця виміру обов'язкова")]
        [StringLength(20, ErrorMessage = "Одиниця виміру не може перевищувати 20 символів")]
        public required string Unit { get; set; }
    }
}
