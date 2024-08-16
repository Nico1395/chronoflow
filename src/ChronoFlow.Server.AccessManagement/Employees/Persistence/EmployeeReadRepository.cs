using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence;

internal sealed class EmployeeReadRepository(DbContext _dbContext) : IEmployeeReadRepository
{
}
