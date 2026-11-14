using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Application.DTOs;
using Teambase.Assessment.Domain.Entities;

namespace Teambase.Assessment.Application.Services;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    public async Task<SaveEmployeeResponse> SaveAsync(SaveEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        Validate(request);

        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            MemberAdditionDate = request.MemberAdditionDate,
            CreatedAtUtc = DateTime.UtcNow
        };

        await repository.AddAsync(employee, cancellationToken);
        return new SaveEmployeeResponse(employee.Id);
    }

    private static void Validate(SaveEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new ArgumentException("First name is required.", nameof(request.FirstName));
        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new ArgumentException("Last name is required.", nameof(request.LastName));
        if (request.MemberAdditionDate > DateOnly.FromDateTime(DateTime.UtcNow.Date))
            throw new ArgumentException("Member addition date cannot be in the future.");
    }
}