﻿using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.AccessManagement.Roles.Entities;

public class RoleViewModel : MainDataViewModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<PermissionViewModel> Permissions { get; set; } = [];

    internal bool HasPermission(PermissionViewModel permission)
    {
        return Permissions.Contains(permission);
    }

    internal void TogglePermission(PermissionViewModel permission)
    {
        if (!Permissions.Remove(permission))
            Permissions.Add(permission);
    }
}
