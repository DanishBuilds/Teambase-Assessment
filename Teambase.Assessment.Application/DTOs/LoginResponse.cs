namespace Teambase.Assessment.Application.DTOs;

public record LoginResponse(string Token, DateTime ExpiresAtUtc);