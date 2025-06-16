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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            await _customerService.AddCustomerAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createCustomerDto }, createCustomerDto);
        }

        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.Id) return BadRequest();
            await _customerService.UpdateCustomerAsync(customerDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}