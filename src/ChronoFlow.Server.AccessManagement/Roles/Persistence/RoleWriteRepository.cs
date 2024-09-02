using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence;

internal sealed class RoleWriteRepository(DbContext _dbContext) : IRoleWriteRepository
{
    public Task AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Add(role);
        foreach (var rolePermission in role.RolePermissions)
            _dbContext.Entry(rolePermission.Permission).State = EntityState.Unchanged;

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Role existingRole, Role updatedRole, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(existingRole).CurrentValues.SetValues(updatedRole);
        _dbContext.SyncCollections(existingRole.RolePermissions, updatedRole.RolePermissions, rp => new { rp.RoleId, rp.PermissionId });

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(role);
        return Task.CompletedTask;
    }
}
