using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Application.DTOs;

public class GetPremiumQuery
{
    public Guid EmployeeId { get; set; }
    public PricingModel PricingModel { get; set; }
    public ProrataMethod ProrataMethod { get; set; }
    public DateOnly PolicyStartDate { get; set; }
    public DateOnly PolicyEndDate { get; set; }
}