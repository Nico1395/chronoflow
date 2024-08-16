namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeEmail
{
    public required Guid EmployeeId { get; set; }
    public required string EmailAddress { get; set; }
}
