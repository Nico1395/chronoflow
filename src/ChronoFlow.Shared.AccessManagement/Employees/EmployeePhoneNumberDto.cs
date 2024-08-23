namespace ChronoFlow.Shared.AccessManagement.Employees;

public record EmployeePhoneNumberDto
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }
}
