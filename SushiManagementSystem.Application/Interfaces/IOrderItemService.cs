using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync();
        Task<OrderItemDto> GetOrderItemByIdAsync(int id);
        Task AddOrderItemAsync(OrderItemDto orderItemDto);
        Task UpdateOrderItemAsync(OrderItemDto orderItemDto);
        Task DeleteOrderItemAsync(int id);
    }
}