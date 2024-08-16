using ChronoFlow.Server.AccessManagement.Roles.Entities;

namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public class Employee
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public EmployeeCredentials Credentials { get; init; } = EmployeeCredentials.Empty();
    public EmployeeName Name { get; init; } = EmployeeName.Empty();
    public List<EmployeeEmail> Emails { get; init; } = [];
    public List<EmployeePhoneNumber> PhoneNumbers { get; set; } = [];
    public List<EmployeeAddress> Addresses { get; init; } = [];
    public List<Role> Roles { get; set; } = [];
    public DateTime? Birthday { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
