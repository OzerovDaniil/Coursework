using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync();
        Task<MenuItemDto> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(MenuItemDto menuItemDto);
        Task UpdateMenuItemAsync(MenuItemDto menuItemDto);
        Task DeleteMenuItemAsync(int id);

        Task<IEnumerable<MenuItemDto>> GetMenuItemsAsync(MenuItemFilterDto filter);
    }
}