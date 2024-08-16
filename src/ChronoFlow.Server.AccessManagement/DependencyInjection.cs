using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.AccessManagement.Employees.Persistence;
using ChronoFlow.Server.AccessManagement.Permissions.Entities.Repositories;
using ChronoFlow.Server.AccessManagement.Permissions.Persistence;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.AccessManagement.Roles.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Server.AccessManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddAccessManagement(this IServiceCollection services)
    {
        services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
        services.AddScoped<IRoleReadRepository, RoleReadRepository>();
        services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();
        services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
        services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();

        return services;
    }
}
