using ChronoFlow.Client.AccessManagement.Permissions;

namespace ChronoFlow.Client.AccessManagement.Roles;

public sealed record RolePermissionModel
{
    public required Guid RoleId { get; init; }
    public required int PermissionId { get; init; }
    public required PermissionModel Permission { get; init; }
}
