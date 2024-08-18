namespace ChronoFlow.Shared.Common.Objects.ValueObjects;

public sealed record AddressDto
{
    public required string? Street { get; set; }
    public required string? HouseNumber { get; set; }
    public required string? City { get; set; }
    public required string? PostalCode { get; set; }
    public required string? State { get; set; }
    public required string? Country { get; set; }
}
