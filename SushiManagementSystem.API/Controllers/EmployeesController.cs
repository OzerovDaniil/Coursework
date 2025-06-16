using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using System.Threading.Tasks;

namespace SushiManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            await _employeeService.AddEmployeeAsync(createEmployeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createEmployeeDto }, createEmployeeDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id) return BadRequest();
            await _employeeService.UpdateEmployeeAsync(employeeDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}