namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeEmail
{
    public required Guid EmployeeId { get; set; }
    public required string Email { get; set; }
    public required bool IsPrimary { get; set; }
}
