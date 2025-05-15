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
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await _unitOfWork.MenuItems.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<MenuItemDto> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _unitOfWork.MenuItems.GetByIdAsync(id);
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task AddMenuItemAsync(MenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            await _unitOfWork.MenuItems.AddAsync(menuItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            _unitOfWork.MenuItems.Update(menuItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _unitOfWork.MenuItems.GetByIdAsync(id);
            if (menuItem != null)
            {
                _unitOfWork.MenuItems.Delete(menuItem);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsAsync()
        {
            var menuItems = await _unitOfWork.MenuItems.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

    }
}