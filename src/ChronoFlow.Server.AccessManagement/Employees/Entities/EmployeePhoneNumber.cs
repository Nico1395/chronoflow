namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeePhoneNumber
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }
}
