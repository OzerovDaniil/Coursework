using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using SushiManagementSystem.Application.Services;
using System.Threading.Tasks;

namespace SushiManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems([FromQuery] MenuItemFilterDto filter)
        {
            // Adjust the arguments below to match the actual method signature of GetMenuItemsAsync
            var menuItems = await _menuService.GetMenuItemsAsync();
            return Ok(menuItems);
        }
    }
}