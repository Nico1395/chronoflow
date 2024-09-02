using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.AccessManagement.Roles.Entities;

public class RoleViewModel : MainDataViewModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<RolePermissionViewModel> RolePermissions { get; set; } = [];

    internal bool HasPermission(PermissionViewModel permission)
    {
        return RolePermissions.Contains(RolePermissionViewModel.Create(Id, permission));
    }

    internal void TogglePermission(PermissionViewModel permission)
    {
        var rolePermission = RolePermissionViewModel.Create(Id, permission);
        if (!RolePermissions.Remove(rolePermission))
            RolePermissions.Add(rolePermission);
    }
}
