using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Повне ім'я обов'язкове")]
        [StringLength(100, ErrorMessage = "Ім'я не може перевищувати 100 символів")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Посада обов'язкова")]
        [StringLength(50, ErrorMessage = "Посада не може перевищувати 50 символів")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Посада може містити лише літери та пробіли")]
        public required string Position { get; set; }

        [Phone(ErrorMessage = "Некоректний номер телефону")]
        public string? PhoneNumber { get; set; }
    }
}