using ChronoFlow.Server.AccessManagement.Roles;

namespace ChronoFlow.Server.AccessManagement.Users;

public sealed class UserRole
{
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
    public required Role role { get; init; }
}
