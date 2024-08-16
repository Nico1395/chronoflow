namespace ChronoFlow.Server.AccessManagement.Permissions.Entities;

public record Permission
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
