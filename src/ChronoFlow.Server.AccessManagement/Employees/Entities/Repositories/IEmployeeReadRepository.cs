namespace ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;

internal interface IEmployeeReadRepository
{
    public Task<List<Employee>> GetAllEagerAsync(CancellationToken cancellationToken = default);
    public Task<Employee?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}
