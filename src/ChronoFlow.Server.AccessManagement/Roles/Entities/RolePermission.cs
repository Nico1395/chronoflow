namespace ChronoFlow.Server.AccessManagement.Roles.Entities;

public record RolePermission
{
    public required Guid RoleId { get; set; }
    public required int PermissionId { get; set; }
}
