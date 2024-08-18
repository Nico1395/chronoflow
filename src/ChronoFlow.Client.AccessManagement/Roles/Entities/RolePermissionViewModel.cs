namespace ChronoFlow.Client.AccessManagement.Roles.Entities;

public record RolePermissionViewModel
{
    public required Guid RoleId { get; set; }
    public required int PermissionId { get; set; }
}
