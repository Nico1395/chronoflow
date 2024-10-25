using ChronoFlow.Shared.AccessManagement.Users;
using ChronoFlow.Shared.Common.Dtos;

namespace ChronoFlow.Shared.EmployeeManagement.Employees;

public sealed class EmployeeDto
{
    public UserDto? User { get; init; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
    public DateTime Birthday { get; set; }
    public EmployeeGender Gender { get; set; } = EmployeeGender.NotSpecified;
    public OptionalAddressDto Address { get; init; } = null!;
    public List<EmployeeEmailAddressDto> EmailAddresses { get; init; } = [];
    public List<EmployeePhoneNumberDto> PhoneNumbers { get; init; } = [];
}
