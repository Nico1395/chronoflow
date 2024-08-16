using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence;

internal sealed class RoleReadRepository(DbContext _dbContext) : IRoleReadRepository
{
}
