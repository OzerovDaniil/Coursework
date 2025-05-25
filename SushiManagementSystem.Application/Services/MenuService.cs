using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using SushiManagementSystem.Domain.Entities;

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

        public async Task UpdateMenuItemAsync(int id, MenuItemDto menuItemDto)
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
        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsAsync(MenuItemFilterDto filter)
        {
            // Создаём выражение фильтра на основе полей filter
            Expression<Func<MenuItem, bool>> predicate = item =>
                (string.IsNullOrEmpty(filter.Name) || item.Name.Contains(filter.Name)) &&
                (!filter.MinPrice.HasValue || item.Price >= filter.MinPrice.Value) &&
                (!filter.MaxPrice.HasValue || item.Price <= filter.MaxPrice.Value);

            // Передаём выражение в репозиторий
            var menuItems = await _unitOfWork.MenuItems.FindAsync(predicate);

            // Преобразуем результат в DTO и возвращаем
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }


    }
}