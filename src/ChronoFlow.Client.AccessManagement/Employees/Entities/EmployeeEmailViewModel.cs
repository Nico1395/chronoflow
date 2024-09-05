namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public record EmployeeEmailViewModel
{
    public required Guid EmployeeId { get; set; }
    public required string Email { get; set; }
    public required bool IsPrimary { get; set; }

    public static EmployeeEmailViewModel New(Guid employeeId)
    {
        return new EmployeeEmailViewModel()
        {
            EmployeeId = employeeId,
            Email = string.Empty,
            IsPrimary = false,
        };
    }
}
