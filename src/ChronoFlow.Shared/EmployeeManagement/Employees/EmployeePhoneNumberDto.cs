namespace ChronoFlow.Shared.EmployeeManagement.Employees;

public sealed class EmployeePhoneNumberDto
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsPrimary { get; set; }
}
