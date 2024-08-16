namespace ChronoFlow.Server.AccessManagement.Permissions.Entities.Repositories;

internal interface IPermissionReadRepository
{
    public Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
}
