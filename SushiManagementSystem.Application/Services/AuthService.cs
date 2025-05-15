using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Domain.Entities;
using SushiManagementSystem.Application.Interfaces;


namespace SushiManagementSystem.Application.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var employee = await _unitOfWork.Employees.GetByUsernameAsync(loginDto.Username);
            if (employee == null || !VerifyPassword(employee, loginDto.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT key is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, employee.Username),
                    new Claim(ClaimTypes.Role, employee.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var employees = new Employee
            {
                Username = registerDto.Username,
                PasswordHash = HashPassword(registerDto.Password),
                Email = registerDto.Email,
                Role = "User",
                PhoneNumber = registerDto.PhoneNumber // Ensure RegisterDto has this property
            };
            await _unitOfWork.Employees.AddAsync(employees);
            await _unitOfWork.SaveChangesAsync();
        }

        private bool VerifyPassword(Employee employees, string password)
        {
            // Реалізуй перевірку пароля (наприклад, BCrypt)
            return true; // Тимчасово
        }

        private string HashPassword(string password)
        {
            // Реалізуй хешування пароля (наприклад, BCrypt)
            return password; // Тимчасово
        }
    }
}