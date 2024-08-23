using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence;

internal sealed class RoleReadRepository(DbContext _dbContext) : IRoleReadRepository
{
    public Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Role>().Include(r => r.Permissions).ToListAsync(cancellationToken);
    }

    public Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Role>().Include(r => r.Permissions).SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
