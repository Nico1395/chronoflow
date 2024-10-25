namespace ChronoFlow.Server.Common.Entities;

public sealed record Address
{
    public required string Street { get; set; }
    public required string HouseNo { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }

    public static Address Empty => new()
    {
        Street = string.Empty,
        HouseNo = string.Empty,
        City = string.Empty,
        PostalCode = string.Empty,
        State = string.Empty,
        Country = string.Empty,
    };
}
