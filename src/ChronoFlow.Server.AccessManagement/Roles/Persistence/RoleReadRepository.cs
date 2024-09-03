using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence;

internal sealed class RoleReadRepository(DbContext _dbContext) : IRoleReadRepository
{
    public Task<List<Role>> GetAllEagerAsync(CancellationToken cancellationToken = default)
    {
        return GetEagerQuery().ToListAsync(cancellationToken);
    }

    public Task<Role?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return GetEagerQuery().SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Role>().AnyAsync(r => r.Id == id, cancellationToken);
    }

    public Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Role>().AnyAsync(r => r.Name == name, cancellationToken);
    }

    private IQueryable<Role> GetEagerQuery()
    {
        return _dbContext.Set<Role>().Include(r => r.RolePermissions).ThenInclude(r => r.Permission);
    }
}
