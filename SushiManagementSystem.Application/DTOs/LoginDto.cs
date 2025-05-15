using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Ім'я користувача обов'язкове")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        public required string Password { get; set; }
    }
}