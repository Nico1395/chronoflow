namespace ChronoFlow.Client.Common.Models;

public sealed class AddressModel
{
    public required string Street { get; set; }
    public required string HouseNo { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
}
