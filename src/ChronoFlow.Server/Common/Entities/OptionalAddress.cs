namespace ChronoFlow.Server.Common.Entities;

public sealed record OptionalAddress
{
    public string? Street { get; set; }
    public string? HouseNo { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    public static OptionalAddress Empty => new();
}
