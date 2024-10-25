using ChronoFlow.Shared.AccessManagement.Permissions;

namespace ChronoFlow.Shared.AccessManagement.Roles;

public sealed record RolePermissionDto
{
    public required Guid RoleId { get; init; }
    public required int PermissionId { get; init; }
    public required PermissionDto Permission { get; init; }
}
