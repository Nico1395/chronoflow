using ChronoFlow.Client.Common.Models;

namespace ChronoFlow.Client.AccessManagement.Roles;

public sealed class RoleModel : MainDataAggregateModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<RolePermissionModel> RolePermissions { get; init; } = [];
}
