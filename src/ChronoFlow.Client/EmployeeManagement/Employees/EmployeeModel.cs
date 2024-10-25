using ChronoFlow.Client.AccessManagement.Users;
using ChronoFlow.Client.Common.Models;
using ChronoFlow.Shared.EmployeeManagement.Employees;

namespace ChronoFlow.Client.EmployeeManagement.Employees;

public sealed class EmployeeModel
{
    public UserModel? User { get; init; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
    public DateTime Birthday { get; set; }
    public EmployeeGender Gender { get; set; } = EmployeeGender.NotSpecified;
    public OptionalAddressModel Address { get; init; } = null!;
    public List<EmployeeEmailAddressModel> EmailAddresses { get; init; } = [];
    public List<EmployeePhoneNumberModel> PhoneNumbers { get; init; } = [];
}
