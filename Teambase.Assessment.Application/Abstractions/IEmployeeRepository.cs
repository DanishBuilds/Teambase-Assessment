using Teambase.Assessment.Domain.Entities;

namespace Teambase.Assessment.Application.Abstractions;

public interface IEmployeeRepository
{
    Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}