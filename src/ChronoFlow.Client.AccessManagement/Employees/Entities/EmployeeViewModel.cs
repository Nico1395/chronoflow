using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Objects.ValueObjects;

namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public sealed class EmployeeViewModel
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string PersonnelNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public EmployeeNameViewModel Name { get; init; } = EmployeeNameViewModel.Empty();
    public List<EmployeeEmailViewModel> Emails { get; init; } = [];
    public List<EmployeePhoneNumberViewModel> PhoneNumbers { get; set; } = [];
    public AddressViewModel Address { get; init; } = AddressViewModel.Empty();
    public List<RoleViewModel> Roles { get; set; } = [];
    public DateTime? Birthday { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
