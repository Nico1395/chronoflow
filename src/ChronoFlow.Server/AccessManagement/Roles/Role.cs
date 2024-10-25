using ChronoFlow.Server.Common.Entities;

namespace ChronoFlow.Server.AccessManagement.Roles;

public sealed class Role : MainDataAggregate
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<RolePermission> RolePermissions { get; init; } = [];
}
