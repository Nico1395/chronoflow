namespace ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;

internal interface IRoleReadRepository
{
    public Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
