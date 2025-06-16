using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task AddOrderAsync(CreateOrderDto createOrderDto);
        Task UpdateOrderAsync(OrderDto orderDto);
        Task DeleteOrderAsync(int id);
    }
}