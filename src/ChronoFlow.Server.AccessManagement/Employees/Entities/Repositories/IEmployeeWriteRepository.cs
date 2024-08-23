namespace ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;

internal interface IEmployeeWriteRepository
{
    public Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Employee existingEmployee, Employee updatedEmployee, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default);
}
