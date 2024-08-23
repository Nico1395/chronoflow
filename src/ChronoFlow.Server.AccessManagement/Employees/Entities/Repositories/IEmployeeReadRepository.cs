namespace ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;

internal interface IEmployeeReadRepository
{
    public Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}
