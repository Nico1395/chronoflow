using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence;

internal sealed class EmployeeWriteRepository(DbContext _dbContext) : IEmployeeWriteRepository
{
    public Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        foreach (var role in employee.EmployeeRoles)
            _dbContext.Entry(role).State = EntityState.Detached;

        _dbContext.Add(employee);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Employee existingEmployee, Employee updatedEmployee, CancellationToken cancellationToken = default)
    {
        // TODO -> What about the value objects?

        _dbContext.Entry(existingEmployee).CurrentValues.SetValues(updatedEmployee);

        _dbContext.SyncCollections(existingEmployee.Emails, updatedEmployee.Emails, e => new { e.EmployeeId, e.Email });
        _dbContext.SyncCollections(existingEmployee.PhoneNumbers, updatedEmployee.PhoneNumbers, e => new { e.EmployeeId, e.PhoneNumber });
        _dbContext.SyncCollections(existingEmployee.EmployeeRoles, updatedEmployee.EmployeeRoles, e => new { e.EmployeeId, e.RoleId });

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        foreach (var role in employee.EmployeeRoles)
            _dbContext.Entry(role).State = EntityState.Detached;

        _dbContext.Remove(employee);
        return Task.CompletedTask;
    }
}
