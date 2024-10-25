namespace ChronoFlow.Shared.EmployeeManagement.Employees;

public sealed class EmployeeEmailAddressDto
{
    public required Guid EmployeeId { get; set; }
    public required string Address { get; set; }
    public bool IsPrimary { get; set; }
}
