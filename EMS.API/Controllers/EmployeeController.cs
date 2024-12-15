
using Microsoft.AspNetCore.Mvc;
using EMS.Business.Interfaces;
using EMS.Repository.Models;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }



        [HttpGet("{employeeId}/addresses")]
        public async Task<IActionResult> GetAddressesByEmployeeId(int employeeId)
        {
            var addresses = await _employeeService.GetAddressesByEmployeeIdAsync(employeeId);
            if (addresses == null || !addresses.Any())
            {
                return NotFound($"No addresses found for Employee with ID {employeeId}.");
            }

            return Ok(addresses);
        }

        [HttpPut("address/{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address updatedAddress)
        {
            if (id != updatedAddress.Id)
            {
                return BadRequest("Address ID in the URL and request body must match.");
            }

            var isUpdated = await _employeeService.UpdateAddressAsync(updatedAddress);
            if (!isUpdated)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpGet("manager/{managerId}/employees")]
        public async Task<IActionResult> GetEmployeesByManagerId(int managerId)
        {
            var employees = await _employeeService.GetEmployeesByManagerIdAsync(managerId);
            if (employees == null || !employees.Any())
            {
                return NotFound($"No employees found for Manager with ID {managerId}.");
            }

            return Ok(employees);
        }


    }
}