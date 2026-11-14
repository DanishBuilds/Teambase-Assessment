using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Application.DTOs;
using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/premium")]
public class PremiumController(IPremiumService premiumService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PremiumResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PremiumResponse>> Get(
        [FromQuery] Guid employeeId,
        [FromQuery] PricingModel pricingModel,
        [FromQuery] ProrataMethod prorataMethod,
        [FromQuery] DateOnly policyStartDate,
        [FromQuery] DateOnly policyEndDate,
        CancellationToken cancellationToken)
    {
        var query = new GetPremiumQuery
        {
            EmployeeId = employeeId,
            PricingModel = pricingModel,
            ProrataMethod = prorataMethod,
            PolicyStartDate = policyStartDate,
            PolicyEndDate = policyEndDate
        };

        try
        {
            var response = await premiumService.GetPremiumAsync(query, cancellationToken);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}