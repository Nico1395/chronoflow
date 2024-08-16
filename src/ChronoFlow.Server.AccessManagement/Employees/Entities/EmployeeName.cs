namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeName
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public static EmployeeName Empty()
    {
        return new EmployeeName()
        {
            FirstName = string.Empty,
            LastName = string.Empty,
        };
    }
}
