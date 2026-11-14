using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Application.DTOs;

namespace Teambase.Assessment.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/employees")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(SaveEmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SaveEmployeeResponse>> Post(
        [FromBody] SaveEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await employeeService.SaveAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}