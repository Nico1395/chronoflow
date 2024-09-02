using ChronoFlow.Client.AccessManagement.Permissions.Entities;

namespace ChronoFlow.Client.AccessManagement.Roles.Entities;

public record RolePermissionViewModel
{
    public required Guid RoleId { get; set; }
    public required int PermissionId { get; set; }
    public required PermissionViewModel Permission { get; set; }

    public static RolePermissionViewModel Create(Guid roleId, PermissionViewModel permission)
    {
        return new RolePermissionViewModel()
        {
            RoleId = roleId,
            PermissionId = permission.Id,
            Permission = permission,
        };
    }
}
