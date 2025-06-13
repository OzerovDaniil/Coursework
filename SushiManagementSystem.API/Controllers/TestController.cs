using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiManagementSystem.Infrastructure.Data;

namespace SushiManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("seed-data")]
    public async Task<IActionResult> GetSeedData()
    {
        var result = new
        {
            Customers = await _context.Customers.CountAsync(),
            Employees = await _context.Employees.CountAsync(),
            MenuItems = await _context.MenuItems.CountAsync(),
            Ingredients = await _context.Ingredients.CountAsync(),
            Inventory = await _context.Inventory.CountAsync(),
            Orders = await _context.Orders.CountAsync(),
            OrderItems = await _context.OrderItems.CountAsync(),
            RecipeItems = await _context.RecipeItems.CountAsync()
        };

        return Ok(result);
    }
}