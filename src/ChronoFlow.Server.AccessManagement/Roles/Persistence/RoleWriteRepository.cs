using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence;

internal sealed class RoleWriteRepository(DbContext _dbContext) : IRoleWriteRepository
{
    public Task AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Add(role);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Role existingRole, Role updatedRole, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(existingRole).CurrentValues.SetValues(updatedRole);

        existingRole.Permissions.Clear();
        foreach (var permission in updatedRole.Permissions)
            existingRole.Permissions.Add(permission);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        //var rolePermissions = role.Permissions.Select(p => new RolePermission() { PermissionId = p.Id, RoleId = role.Id});

        _dbContext.Remove(role);
        //_dbContext.RemoveRange(rolePermissions);

        return Task.CompletedTask;
    }
}
