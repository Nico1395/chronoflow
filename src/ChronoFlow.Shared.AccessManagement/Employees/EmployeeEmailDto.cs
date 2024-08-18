namespace ChronoFlow.Shared.AccessManagement.Employees;

public record EmployeeEmailDto
{
    public required Guid EmployeeId { get; set; }
    public required string Email { get; set; }
    public required bool IsPrimary { get; set; }
}
