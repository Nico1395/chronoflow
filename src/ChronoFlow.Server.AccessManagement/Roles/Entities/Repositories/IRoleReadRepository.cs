namespace ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;

internal interface IRoleReadRepository
{
    internal Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    internal Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    internal Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    internal Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
}
