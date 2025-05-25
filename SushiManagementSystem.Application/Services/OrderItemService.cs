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
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync()
        {
            var orderItems = await _unitOfWork.OrderItems.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        }

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(id);
            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task AddOrderItemAsync(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            await _unitOfWork.OrderItems.AddAsync(orderItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            _unitOfWork.OrderItems.Update(orderItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(id);
            if (orderItem != null)
            {
                _unitOfWork.OrderItems.Delete(orderItem);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}