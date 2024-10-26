using ChronoFlow.Server.AccessManagement.Permissions;
using ChronoFlow.Server.AccessManagement.Roles;
using ChronoFlow.Server.AccessManagement.Users;
using ChronoFlow.Shared.AccessManagement.Permissions;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.AccessManagement.Users;
using ChronoFlow.Shared.Common.Mapping.Configuration;

namespace ChronoFlow.Server.AccessManagement;

internal sealed class AccessManagementMappingProfile : IMappingProfile
{
    public void ConfigureMapping(IMappingProfileConfiguration configuration)
    {
        configuration.ConfigureTypes<Permission, PermissionDto>();
        configuration.ConfigureTypes<Role, RoleDto>();
        configuration.ConfigureTypes<RolePermission, RolePermissionDto>();
        configuration.ConfigureTypes<User, UserDto>();
        configuration.ConfigureTypes<UserRole, UserRoleDto>();
        configuration.ConfigureTypes<UserCredentials, UserCredentialsDto>();
    }
}
