using Microsoft.EntityFrameworkCore;
using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Domain.Entities;
using Teambase.Assessment.Infrastructure.Persistence;

namespace Teambase.Assessment.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
{
    public async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync(cancellationToken);
        return employee;
    }

    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
}