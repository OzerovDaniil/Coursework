using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class CreateCustomerDto
    {

        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [StringLength(100, ErrorMessage = "Ім'я не може перевищувати 100 символів")]
        public required string Name { get; set; }

        public string? Surname { get; set; }

        [Required(ErrorMessage = "Username обов'язковий")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        public required string PasswordHash { get; set; }

        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Телефон обов'язковий")]
        [Phone(ErrorMessage = "Некоректний номер телефону")]
        public required string PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}