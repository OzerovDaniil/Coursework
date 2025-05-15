using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ім'я користувача обов'язкове")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен містити щонайменше 6 символів")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Електронна пошта обов'язкова")]
        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}