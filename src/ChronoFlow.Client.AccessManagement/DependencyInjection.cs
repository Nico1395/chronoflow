using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataList;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.AccessManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddAccessManagement(this IServiceCollection services)
    {
        services.AddScoped<IMainDataListService<EmployeeViewModel>, EmployeeMainDataListService>();

        return services;
    }
}
