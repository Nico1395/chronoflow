namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeRole
{
    public Guid EmployeeId { get; init; }
    public Guid RoleId { get; init; }
}
