namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public record EmployeePhoneNumberViewModel
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }
}
