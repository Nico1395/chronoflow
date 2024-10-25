namespace ChronoFlow.Server.EmployeeManagement.Employees;

public sealed class EmployeePhoneNumber
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsPrimary { get; set; }
}
