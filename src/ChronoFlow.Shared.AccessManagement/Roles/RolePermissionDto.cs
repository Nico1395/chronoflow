namespace ChronoFlow.Shared.AccessManagement.Roles;

public record RolePermissionDto
{
    public required Guid RoleId { get; set; }
    public required int PermissionId { get; set; }
}
