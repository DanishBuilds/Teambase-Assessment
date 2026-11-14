using Teambase.Assessment.Application.DTOs;

namespace Teambase.Assessment.Application.Abstractions;

public interface IPremiumService
{
    Task<PremiumResponse> GetPremiumAsync(GetPremiumQuery query, CancellationToken cancellationToken = default);
}