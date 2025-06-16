using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class CreateInventoryDto
    {
        [Required(ErrorMessage = "Ідентифікатор інгредієнта обов'язковий")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Кількість обов'язкова")]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Дата оновлення обов'язкова")]
        public DateTime LastUpdated { get; set; }

        [Required(ErrorMessage = "Назва товару обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public required string ItemName { get; set; }

        [Required(ErrorMessage = "Одиниця виміру обов'язкова")]
        [StringLength(20, ErrorMessage = "Одиниця виміру не може перевищувати 20 символів")]
        public required string Unit { get; set; }
    }
}