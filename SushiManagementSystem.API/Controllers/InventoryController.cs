using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using System.Threading.Tasks;

namespace SushiManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllInventoryItems()
        {
            var inventoryItems = await _inventoryService.GetAllInventoryItemsAsync();
            return Ok(inventoryItems);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryItemById(int id)
        {
            var inventoryItem = await _inventoryService.GetInventoryItemByIdAsync(id);
            if (inventoryItem == null) return NotFound();
            return Ok(inventoryItem);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddInventoryItem([FromBody] InventoryDto inventoryDto)
        {
            await _inventoryService.AddInventoryItemAsync(inventoryDto);
            return CreatedAtAction(nameof(GetInventoryItemById), new { id = inventoryDto.Id }, inventoryDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, [FromBody] InventoryDto inventoryDto)
        {
            if (id != inventoryDto.Id) return BadRequest();
            await _inventoryService.UpdateInventoryItemAsync(inventoryDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            await _inventoryService.DeleteInventoryItemAsync(id);
            return NoContent();
        }
    }
}