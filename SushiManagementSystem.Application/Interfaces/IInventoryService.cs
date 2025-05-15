using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllInventoryItemsAsync();
        Task<InventoryDto> GetInventoryItemByIdAsync(int id);
        Task AddInventoryItemAsync(InventoryDto inventoryDto);
        Task UpdateInventoryItemAsync(InventoryDto inventoryDto);
        Task DeleteInventoryItemAsync(int id);
    }
}
