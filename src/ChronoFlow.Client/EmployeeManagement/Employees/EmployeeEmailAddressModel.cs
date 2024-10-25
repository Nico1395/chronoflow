namespace ChronoFlow.Client.EmployeeManagement.Employees;

public sealed class EmployeeEmailAddressModel
{
    public required Guid EmployeeId { get; set; }
    public required string Address { get; set; }
    public bool IsPrimary { get; set; }
}
