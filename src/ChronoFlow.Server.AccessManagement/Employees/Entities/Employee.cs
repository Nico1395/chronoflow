using ChronoFlow.Server.Common.Objects.ValueObjects;

namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public class Employee
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string PersonnelNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public EmployeeName Name { get; init; } = EmployeeName.Empty();
    public List<EmployeeEmail> Emails { get; init; } = [];
    public List<EmployeePhoneNumber> PhoneNumbers { get; set; } = [];
    public Address Address { get; init; } = Address.Empty();
    public List<EmployeeRole> EmployeeRoles { get; set; } = [];
    public DateTime? Birthday { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
