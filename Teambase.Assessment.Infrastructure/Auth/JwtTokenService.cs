using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Teambase.Assessment.Application.Abstractions;

namespace Teambase.Assessment.Infrastructure.Auth;

public class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public AuthTokenResult? CreateToken(string username, string password)
    {
        var expectedUser = configuration["Auth:DevUser:Username"];
        var expectedPassword = configuration["Auth:DevUser:Password"];
        if (!string.Equals(username, expectedUser, StringComparison.Ordinal)
            || !string.Equals(password, expectedPassword, StringComparison.Ordinal))
        {
            return null;
        }

        var signingKey = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is not configured.");

        var issuer = configuration["Jwt:Issuer"] ?? "Teambase.Assessment";
        var audience = configuration["Jwt:Audience"] ?? "Teambase.Assessment.Api";
        var expiresMinutes = int.TryParse(configuration["Jwt:ExpiresMinutes"], out var minutes) ? minutes : 120;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            new[] { new Claim(ClaimTypes.Name, username) },
            expires: expires,
            signingCredentials: credentials);

        return new AuthTokenResult(new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}