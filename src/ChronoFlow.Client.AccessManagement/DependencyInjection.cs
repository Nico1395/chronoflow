using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Employees.Persistence;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Persistence;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.AccessManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddAccessManagement(this IServiceCollection services)
    {
        services.AddScoped<IMainDataListService<EmployeeViewModel>, MainDataListService<EmployeeViewModel>>();
        services.AddScoped<IMainDataFormService<EmployeeViewModel>, MainDataFormService<EmployeeViewModel>>();
        services.AddScoped<IMainDataViewModelService<EmployeeViewModel>, EmployeeMainDataViewModelService>();

        services.AddScoped<IMainDataListService<RoleViewModel>, MainDataListService<RoleViewModel>>();
        services.AddScoped<IMainDataFormService<RoleViewModel>, MainDataFormService<RoleViewModel>>();
        services.AddScoped<IMainDataViewModelService<RoleViewModel>, RoleMainDataViewModelService>();

        return services;
    }
}
