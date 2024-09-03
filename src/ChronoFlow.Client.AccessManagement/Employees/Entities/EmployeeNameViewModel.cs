namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public sealed record EmployeeNameViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public static EmployeeNameViewModel Empty()
    {
        return new EmployeeNameViewModel()
        {
            FirstName = string.Empty,
            LastName = string.Empty,
        };
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}".Trim();
    }
}
