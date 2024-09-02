using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.AccessManagement.Permissions;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using Mapster;

namespace ChronoFlow.Client.AccessManagement;

internal sealed class MappingConfiguration : MappingConfigurationBase
{
    public override void Configure(TypeAdapterConfig mappingConfiguration)
    {
        mappingConfiguration.ConfigureTypes<EmployeeViewModel, EmployeeDto>();
        mappingConfiguration.ConfigureTypes<EmployeeNameViewModel, EmployeeNameDto>();
        mappingConfiguration.ConfigureTypes<EmployeeEmailViewModel, EmployeeEmailDto>();
        mappingConfiguration.ConfigureTypes<EmployeePhoneNumberViewModel, EmployeePhoneNumberDto>();
        mappingConfiguration.ConfigureTypes<PermissionViewModel, PermissionDto>();
        mappingConfiguration.ConfigureTypes<RoleViewModel, RoleDto>();
        mappingConfiguration.ConfigureTypes<RolePermissionViewModel, RolePermissionDto>();
    }
}
