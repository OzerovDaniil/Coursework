using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);
    }
}