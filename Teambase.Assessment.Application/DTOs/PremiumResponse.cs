using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Application.DTOs;

public record PremiumResponse(
    Guid EmployeeId,
    PricingModel PricingModel,
    ProrataMethod ProrataMethod,
    decimal FullPremium,
    decimal ProratedPremium,
    PremiumBreakdown Details);

public record PremiumBreakdown(
    int Age,
    int RemainingDays,
    int RemainingMonths,
    DateOnly PolicyStartDate,
    DateOnly PolicyEndDate,
    DateOnly MemberAdditionDate);