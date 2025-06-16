using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CreateCustomerDto createCustomerDto);
        Task UpdateCustomerAsync(CustomerDto customerDto);
        Task DeleteCustomerAsync(int id);
    }
}