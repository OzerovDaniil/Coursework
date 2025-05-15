using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SushiManagementSystem.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [StringLength(100, ErrorMessage = "Ім'я не може перевищувати 100 символів")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Некоректний номер телефону")]
        public string? PhoneNumber { get; set; }
    }
}