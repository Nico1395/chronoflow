namespace ChronoFlow.Client.AccessManagement.Permissions.Entities;

public record PermissionViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
