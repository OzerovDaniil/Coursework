using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using SushiManagementSystem.API.Filters;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMenuItems([FromQuery] MenuItemFilterDto filter)
        {
            var menuItems = await _menuService.GetMenuItemsAsync(filter);
            return Ok(menuItems);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null) return NotFound();
            return Ok(menuItem);
        }

        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDto createMenuItemDto)
        {
            await _menuService.AddMenuItemAsync(createMenuItemDto);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = createMenuItemDto }, createMenuItemDto);
        }

        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemDto)
        {
            if (id != menuItemDto.Id) return BadRequest();
            await _menuService.UpdateMenuItemAsync(id, menuItemDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuService.DeleteMenuItemAsync(id);
            return NoContent();
        }
    }
}