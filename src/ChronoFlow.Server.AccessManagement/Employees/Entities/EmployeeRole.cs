using ChronoFlow.Server.AccessManagement.Roles.Entities;

namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeRole
{
    public required Guid EmployeeId { get; set; }
    public required Employee Employee { get; set; }
    public required Guid RoleId { get; set; }
    public required Role Role { get; set; }
}
