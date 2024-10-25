using ChronoFlow.Shared.AccessManagement.Roles;

namespace ChronoFlow.Shared.AccessManagement.Users;

public sealed class UserRoleDto
{
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
    public required RoleModel role { get; init; }
}
