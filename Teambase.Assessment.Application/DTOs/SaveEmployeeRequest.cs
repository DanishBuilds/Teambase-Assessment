using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Application.DTOs;

public class SaveEmployeeRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public DateOnly MemberAdditionDate { get; set; }
}