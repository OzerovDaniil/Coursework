using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Username обов'язковий")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        public required string PasswordHash { get; set; }

        [Required(ErrorMessage = "Роль обов'язкова")]
        public required string Role { get; set; }

        [Required(ErrorMessage = "Email обов'язковий")]
        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Телефон обов'язковий")]
        [Phone(ErrorMessage = "Некоректний номер телефону")]
        public required string PhoneNumber { get; set; }
    }
}