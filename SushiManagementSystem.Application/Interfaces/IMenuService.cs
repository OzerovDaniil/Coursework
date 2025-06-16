using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync();
        Task<MenuItemDto> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(CreateMenuItemDto createMenuItemDto);
        Task UpdateMenuItemAsync(int id, MenuItemDto menuItemDto);
        Task DeleteMenuItemAsync(int id);
        Task<IEnumerable<MenuItemDto>> GetMenuItemsAsync(MenuItemFilterDto filter);

    }
}