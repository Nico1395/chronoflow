using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Employees.Persistence;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.AccessManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddAccessManagement(this IServiceCollection services)
    {
        services.AddScoped<IMainDataListService<EmployeeViewModel>, MainDataListService<EmployeeViewModel>>();
        services.AddScoped<IMainDataViewModelService<EmployeeViewModel>, EmployeeMainDataViewModelService>();

        return services;
    }
}
