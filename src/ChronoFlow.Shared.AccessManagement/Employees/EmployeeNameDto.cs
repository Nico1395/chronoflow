namespace ChronoFlow.Shared.AccessManagement.Employees;

public record EmployeeNameDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
