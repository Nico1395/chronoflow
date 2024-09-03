using ChronoFlow.Client.AccessManagement.Roles.Entities;

namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public record EmployeeRoleViewModel
{
    public required Guid EmployeeId { get; set; }
    public required Guid RoleId { get; set; }
    public required RoleViewModel Role { get; set; }

    public static EmployeeRoleViewModel Create(Guid employeeId, RoleViewModel role)
    {
        return new EmployeeRoleViewModel()
        {
            EmployeeId = employeeId,
            RoleId = role.Id,
            Role = role,
        };
    }
}
