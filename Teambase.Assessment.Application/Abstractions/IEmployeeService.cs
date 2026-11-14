using Teambase.Assessment.Application.DTOs;

namespace Teambase.Assessment.Application.Abstractions;

public interface IEmployeeService
{
    Task<SaveEmployeeResponse> SaveAsync(SaveEmployeeRequest request, CancellationToken cancellationToken = default);
}