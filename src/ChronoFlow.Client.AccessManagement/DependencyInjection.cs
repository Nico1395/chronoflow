using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Employees.Persistence;
using ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataForm;
using ChronoFlow.Client.AccessManagement.Permissions.Persistence;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Persistence;
using ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataForm;
using ChronoFlow.Client.Common.MainData;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.AccessManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddAccessManagement(this IServiceCollection services)
    {
        services.AddScoped<IMainDataViewModelService<EmployeeViewModel>, EmployeeMainDataViewModelService>();
        services.AddScoped<IMainDataListService<EmployeeViewModel>, MainDataListService<EmployeeViewModel>>();
        services.AddMainDataFormService<EmployeeViewModel, IEmployeeMainDataFormService, EmployeeMainDataFormService>();
        services.AddMainDataFormContext<EmployeeViewModel>();

        services.AddScoped<IMainDataViewModelService<RoleViewModel>, RoleMainDataViewModelService>();
        services.AddScoped<IMainDataListService<RoleViewModel>, MainDataListService<RoleViewModel>>();
        services.AddMainDataFormService<RoleViewModel, IRoleMainDataFormService, RoleMainDataFormService>();
        services.AddMainDataFormContext<RoleViewModel>();

        services.AddScoped<IPermissionMainDataViewModelService, PermissionMainDataViewModelService>();

        return services;
    }
}
