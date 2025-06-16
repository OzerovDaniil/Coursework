using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using SushiManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task AddCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var createCustomer = _mapper.Map<Customer>(createCustomerDto);
            await _unitOfWork.Customers.AddAsync(createCustomer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer != null)
            {
                _unitOfWork.Customers.Delete(customer);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}