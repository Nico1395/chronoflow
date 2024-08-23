namespace ChronoFlow.Client.Common.Objects.ValueObjects;

public class AddressViewModel
{
    public required string? Street { get; set; }
    public required string? HouseNumber { get; set; }
    public required string? City { get; set; }
    public required string? PostalCode { get; set; }
    public required string? State { get; set; }
    public required string? Country { get; set; }

    public static AddressViewModel Empty()
    {
        return new AddressViewModel()
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
