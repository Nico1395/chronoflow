using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence;

internal sealed class EmployeeWriteRepository(DbContext _dbContext) : IEmployeeWriteRepository
{
    public Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        foreach (var role in employee.Roles)
            _dbContext.Entry(role).State = EntityState.Detached;

        _dbContext.Add(employee);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Employee existingEmployee, Employee updatedEmployee, CancellationToken cancellationToken = default)
    {
        // TODO -> What about the value objects?

        _dbContext.Entry(existingEmployee).CurrentValues.SetValues(updatedEmployee);

        existingEmployee.Emails.Clear();
        updatedEmployee.Emails.ForEach(existingEmployee.Emails.Add);

        existingEmployee.PhoneNumbers.Clear();
        updatedEmployee.PhoneNumbers.ForEach(existingEmployee.PhoneNumbers.Add);

        existingEmployee.Roles.Clear();
        updatedEmployee.Roles.ForEach(existingEmployee.Roles.Add);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        foreach (var role in employee.Roles)
            _dbContext.Entry(role).State = EntityState.Detached;

        _dbContext.Remove(employee);
        return Task.CompletedTask;
    }
}
