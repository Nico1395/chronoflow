namespace ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;

internal interface IRoleWriteRepository
{
    internal Task AddAsync(Role role, CancellationToken cancellationToken = default);
    internal Task UpdateAsync(Role existingRole, Role updatedRole, CancellationToken cancellationToken = default);
    internal Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
}
