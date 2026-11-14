namespace Teambase.Assessment.Application.Abstractions;

public interface ITokenService
{
    AuthTokenResult? CreateToken(string username, string password);
}

public record AuthTokenResult(string AccessToken, DateTime ExpiresAtUtc);