using ChronoFlow.Shared.AccessManagement.Roles;

namespace ChronoFlow.Shared.AccessManagement.Employees;

public record EmployeeRoleDto
{
    public required Guid EmployeeId { get; set; }
    public required Guid RoleId { get; set; }
    public required RoleDto Role { get; set; }
}