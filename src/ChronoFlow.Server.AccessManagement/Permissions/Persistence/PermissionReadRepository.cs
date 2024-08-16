using ChronoFlow.Server.AccessManagement.Permissions.Entities;
using ChronoFlow.Server.AccessManagement.Permissions.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Permissions.Persistence;

internal sealed class PermissionReadRepository(DbContext _dbContext) : IPermissionReadRepository
{
    public Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Permission>().ToListAsync(cancellationToken);
    }
}
