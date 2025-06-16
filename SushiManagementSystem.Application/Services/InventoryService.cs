using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using SushiManagementSystem.Domain.Entities;
using AutoMapper;

namespace SushiManagementSystem.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllInventoryItemsAsync()
        {
            var inventories = await _unitOfWork.Inventories.GetAllAsync();
            return _mapper.Map<IEnumerable<InventoryDto>>(inventories);
        }

        public async Task<InventoryDto> GetInventoryItemByIdAsync(int id)
        {
            var inventory = await _unitOfWork.Inventories.GetByIdAsync(id);
            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task AddInventoryItemAsync(CreateInventoryDto createInventoryDto)
        {
            var inventory = _mapper.Map<Inventory>(createInventoryDto);
            await _unitOfWork.Inventories.AddAsync(inventory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateInventoryItemAsync(InventoryDto inventoryDto)
        {
            var inventory = _mapper.Map<Inventory>(inventoryDto);
            _unitOfWork.Inventories.Update(inventory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteInventoryItemAsync(int id)
        {
            var inventory = await _unitOfWork.Inventories.GetByIdAsync(id);
            if (inventory != null)
            {
                _unitOfWork.Inventories.Delete(inventory);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}