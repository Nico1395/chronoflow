namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public record EmployeePhoneNumberViewModel
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }

    public static EmployeePhoneNumberViewModel New(Guid employeeId)
    {
        return new EmployeePhoneNumberViewModel()
        {
            EmployeeId = employeeId,
            PhoneNumber = string.Empty,
            //IsPrimary = false,
        };
    }
}
