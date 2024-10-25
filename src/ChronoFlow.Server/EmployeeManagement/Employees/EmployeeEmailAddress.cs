namespace ChronoFlow.Server.EmployeeManagement.Employees;

public sealed class EmployeeEmailAddress
{
    public required Guid EmployeeId { get; set; }
    public required string Address { get; set; }
    public bool IsPrimary { get; set; }
}
