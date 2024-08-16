namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeCredentials
{
    public required string PersonnelNumber { get; set; }
    public required string PasswordHash { get; set; }

    public static EmployeeCredentials Empty()
    {
        return new EmployeeCredentials()
        {
            PersonnelNumber = string.Empty,
            PasswordHash = string.Empty,
        };
    }
}
