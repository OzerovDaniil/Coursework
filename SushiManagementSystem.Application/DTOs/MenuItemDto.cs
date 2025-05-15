using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Ціна обов'язкова")]
        [Range(0.01, 10000, ErrorMessage = "Ціна повинна бути від 0.01 до 10000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Опис обов'язковий")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Категорія обов'язкова")]
        public required string Category { get; set; }

        public bool IsAvailable { get; set; }
    }
}