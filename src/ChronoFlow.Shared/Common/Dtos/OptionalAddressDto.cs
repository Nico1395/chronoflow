namespace ChronoFlow.Shared.Common.Dtos;

public sealed record OptionalAddressDto
{
    public string? Street { get; set; }
    public string? HouseNo { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
