using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Permissions.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.AccessManagement.Permissions;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using Mapster;

namespace ChronoFlow.Server.AccessManagement;

internal sealed class MappingConfiguration : MappingConfigurationBase
{
    public override void Configure(TypeAdapterConfig mappingConfiguration)
    {
        mappingConfiguration.ConfigureTypes<Employee, EmployeeDto>();
        mappingConfiguration.ConfigureTypes<EmployeeName, EmployeeNameDto>();
        mappingConfiguration.ConfigureTypes<EmployeeEmail, EmployeeEmailDto>();
        mappingConfiguration.ConfigureTypes<EmployeePhoneNumber, EmployeePhoneNumberDto>();
        mappingConfiguration.ConfigureTypes<Permission, PermissionDto>();
        mappingConfiguration.ConfigureTypes<Role, RoleDto>();
    }
}
