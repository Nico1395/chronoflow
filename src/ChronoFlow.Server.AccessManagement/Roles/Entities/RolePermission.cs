using ChronoFlow.Server.AccessManagement.Permissions.Entities;

namespace ChronoFlow.Server.AccessManagement.Roles.Entities;

public record RolePermission
{
    public required Guid RoleId { get; set; }
    public required Role Role { get; set; }
    public required int PermissionId { get; set; }
    public required Permission Permission { get; set; }
}
