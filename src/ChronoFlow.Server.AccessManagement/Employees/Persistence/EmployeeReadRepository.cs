using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence;

internal sealed class EmployeeReadRepository(DbContext _dbContext) : IEmployeeReadRepository
{
    public Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return AsEager(_dbContext.Set<Employee>()).ToListAsync(cancellationToken);
    }

    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return AsEager(_dbContext.Set<Employee>()).SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Employee>().AnyAsync(e => e.Id == id, cancellationToken);
    }

    private IQueryable<Employee> AsEager(IQueryable<Employee> query)
    {
        return query
            .Include(e => e.Emails)
            .Include(e => e.PhoneNumbers)
            .Include(e => e.Roles).ThenInclude(r => r.RolePermissions);
    }
}
