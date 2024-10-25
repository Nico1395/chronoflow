using ChronoFlow.Server.AccessManagement.Users;
using ChronoFlow.Server.Common.Entities;
using ChronoFlow.Shared.EmployeeManagement.Employees;

namespace ChronoFlow.Server.EmployeeManagement.Employees;

public sealed class Employee : MainDataAggregate
{
    public User? User { get; init; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
    public DateTime Birthday { get; set; }
    public EmployeeGender Gender { get; set; } = EmployeeGender.NotSpecified;
    public OptionalAddress Address { get; init; } = OptionalAddress.Empty;
    public List<EmployeeEmailAddress> EmailAddresses { get; init; } = [];
    public List<EmployeePhoneNumber> PhoneNumbers { get; init; } = [];
}
