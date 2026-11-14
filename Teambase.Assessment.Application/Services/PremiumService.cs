using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Application.DTOs;

namespace Teambase.Assessment.Application.Services;

public class PremiumService(
    IEmployeeRepository repository,
    PremiumCalculator calculator) : IPremiumService
{
    public async Task<PremiumResponse> GetPremiumAsync(GetPremiumQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await repository.GetByIdAsync(query.EmployeeId, cancellationToken)
            ?? throw new KeyNotFoundException($"Employee '{query.EmployeeId}' was not found.");

        var result = calculator.Calculate(
            employee.DateOfBirth,
            employee.Gender,
            employee.MemberAdditionDate,
            query.PolicyStartDate,
            query.PolicyEndDate,
            query.PricingModel,
            query.ProrataMethod);

        return new PremiumResponse(
            employee.Id,
            query.PricingModel,
            query.ProrataMethod,
            result.FullPremium,
            result.ProratedPremium,
            new PremiumBreakdown(
                result.Age,
                result.RemainingDays,
                result.RemainingMonths,
                query.PolicyStartDate,
                query.PolicyEndDate,
                employee.MemberAdditionDate));
    }
}