namespace ChronoFlow.Server.AccessManagement.Permissions;

public sealed record Permission
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}
