namespace ChronoFlow.Server.Common.Objects.ValueObjects;

public sealed record Address
{
    public required string? Street { get; set; }
    public required string? HouseNumber { get; set; }
    public required string? City { get; set; }
    public required string? PostalCode { get; set; }
    public required string? State { get; set; }
    public required string? Country { get; set; }

    public static Address Empty()
    {
        return new Address()
        {
            Street = null,
            HouseNumber = null,
            City = null,
            PostalCode = null,
            Country = null,
            State = null,
        };
    }
}
