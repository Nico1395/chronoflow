using ChronoFlow.Client.AccessManagement.Permissions;
using ChronoFlow.Client.AccessManagement.Roles;
using ChronoFlow.Client.AccessManagement.Users;
using ChronoFlow.Shared.AccessManagement.Permissions;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.AccessManagement.Users;
using ChronoFlow.Shared.Common.Mapping.Configuration;

namespace ChronoFlow.Client.AccessManagement;

internal sealed class AccessManagementMappingProfile : IMappingProfile
{
    public void ConfigureMapping(IMappingProfileConfiguration configuration)
    {
        configuration.ConfigureTypes<PermissionModel, PermissionDto>();
        configuration.ConfigureTypes<RoleModel, RoleDto>();
        configuration.ConfigureTypes<RolePermissionModel, RolePermissionDto>();
        configuration.ConfigureTypes<PermissionModel, PermissionDto>();
        configuration.ConfigureTypes<UserModel, UserDto>();
        configuration.ConfigureTypes<UserCredentialsModel, UserCredentialsDto>();
        configuration.ConfigureTypes<UserRoleModel, UserRoleDto>();
    }
}
