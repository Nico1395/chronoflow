using ChronoFlow.Client.AccessManagement.Roles;

namespace ChronoFlow.Client.AccessManagement.Users;

public sealed class UserRoleModel
{
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
    public required RoleModel role { get; init; }
}
