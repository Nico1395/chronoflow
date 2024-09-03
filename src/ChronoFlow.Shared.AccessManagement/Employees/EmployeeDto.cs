using ChronoFlow.Shared.Common.Objects.ValueObjects;

namespace ChronoFlow.Shared.AccessManagement.Employees;

public class EmployeeDto
{
    public Guid Id { get; init; }
    public string PersonnelNumber { get; set; } = string.Empty;
    public required EmployeeNameDto Name { get; init; }
    public List<EmployeeEmailDto> Emails { get; init; } = [];
    public List<EmployeePhoneNumberDto> PhoneNumbers { get; set; } = [];
    public required AddressDto Address { get; init; }
    public List<EmployeeRoleDto> EmployeeRoles { get; set; } = [];
    public DateTime? Birthday { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
