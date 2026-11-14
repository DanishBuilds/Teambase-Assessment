using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Application.DTOs;

namespace Teambase.Assessment.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ITokenService tokenService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        var result = tokenService.CreateToken(request.Username, request.Password);
        if (result is null)
            return Unauthorized(new { message = "Invalid username or password." });

        return Ok(new LoginResponse(result.AccessToken, result.ExpiresAtUtc));
    }
}