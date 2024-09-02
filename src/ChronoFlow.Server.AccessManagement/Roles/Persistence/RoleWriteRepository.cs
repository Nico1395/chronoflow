using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
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

        foreach (var updatedRolePermission in updatedRole.RolePermissions)
        {
            var existingRolePermission = existingRole.RolePermissions.FirstOrDefault(rp => rp.PermissionId == updatedRolePermission.PermissionId);
            if (existingRolePermission == null)
                existingRole.RolePermissions.Add(updatedRolePermission);
            else
                _dbContext.Entry(existingRolePermission).State = EntityState.Unchanged;
        }

        foreach (var existingRolePermission in existingRole.RolePermissions.ToList())
        {
            if (!updatedRole.RolePermissions.Any(rp => rp.PermissionId == existingRolePermission.PermissionId))
                existingRole.RolePermissions.Remove(existingRolePermission);
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(role);
        return Task.CompletedTask;
    }
}
