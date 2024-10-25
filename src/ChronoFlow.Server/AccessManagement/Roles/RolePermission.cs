using ChronoFlow.Server.AccessManagement.Permissions;

namespace ChronoFlow.Server.AccessManagement.Roles;

public sealed record RolePermission
{
    public required Guid RoleId { get; init; }
    public required int PermissionId { get; init; }
    public required Permission Permission { get; init; }
}
