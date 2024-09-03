using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence;

internal sealed class EmployeeReadRepository(DbContext _dbContext) : IEmployeeReadRepository
{
    public Task<List<Employee>> GetAllEagerAsync(CancellationToken cancellationToken = default)
    {
        return GetEagerQuery().ToListAsync(cancellationToken);
    }

    public Task<Employee?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return GetEagerQuery().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Employee>().AnyAsync(e => e.Id == id, cancellationToken);
    }

    private IQueryable<Employee> GetEagerQuery()
    {
        return _dbContext.Set<Employee>()
            .Include(e => e.Emails)
            .Include(e => e.PhoneNumbers)
            .Include(e => e.EmployeeRoles)
                .ThenInclude(r => r.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(r => r.Permission);
    }

    private IQueryable<Employee> GetQuery()
    {
        return _dbContext.Set<Employee>();
    }
}
