namespace ChronoFlow.Shared.AccessManagement.Permissions;

public record PermissionDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
