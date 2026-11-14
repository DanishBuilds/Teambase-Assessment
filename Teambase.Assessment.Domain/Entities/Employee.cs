using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public DateOnly MemberAdditionDate { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}