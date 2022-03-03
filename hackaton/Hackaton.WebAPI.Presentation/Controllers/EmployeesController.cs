using Hackaton.Application.DataTransferObject;
using Hackaton.Application.Interfaces.Services;
using Hackaton.Application.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hackaton.WebAPI.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// GET /api/Employees
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <returns>A list of employees</returns>
        /// <response Code = "200">Returns a list of employees</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        public async Task<IActionResult> GetAllEmployeesAsync([FromQuery]EmployeeParameters employeeParameters, CancellationToken cancellationToken)
        {
            var (employees , metaData) = await _service.EmployeeService.GetAllAsync(employeeParameters, cancellationToken);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
            return Ok(employees);
        }

        /// <summary>
        /// Gets an employee by his employeeId.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// GET /api/Employees/103C444A-0000-4E47-3416-08D9EB1AD5AE
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="employeeId"></param>
        /// <returns>An employee.</returns>
        /// <response Code = "200">Returns a list of employees</response>
        /// <response Code = "404">Returns "Employee was not found"</response>
        /// <response Code = "500">Returns internal server error</response>
        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetEmployeeAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var result = await _service.EmployeeService.GetByIdAsync(employeeId, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeForCreationDto employeeToPost, CancellationToken cancellationToken)
        {
            var result = await _service.EmployeeService.CreateAsync(employeeToPost, cancellationToken);
            return CreatedAtRoute("GetEmployeeById", new { employeeId = result.Id }, result);
        }

        [HttpDelete("{employeeId}", Name = "DeleteEmployeeById")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            await _service.EmployeeService.DeleteAsync(employeeId, cancellationToken);
            return NoContent();
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeForUpdateDto employeeToUpdate, Guid employeeId, CancellationToken cancellationToken)
        {
            await _service.EmployeeService.UpdateAsync(employeeId, employeeToUpdate, cancellationToken);
            return NoContent();
        }
    }
}