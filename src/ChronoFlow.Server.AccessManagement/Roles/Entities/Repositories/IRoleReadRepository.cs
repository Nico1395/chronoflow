namespace ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;

internal interface IRoleReadRepository
{
    internal Task<List<Role>> GetAllEagerAsync(CancellationToken cancellationToken = default);
    internal Task<Role?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken = default);
    internal Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    internal Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
}
