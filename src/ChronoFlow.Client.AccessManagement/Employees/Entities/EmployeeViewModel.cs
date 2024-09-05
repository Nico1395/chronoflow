using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.MainData.Entities;
using ChronoFlow.Client.Common.Objects.ValueObjects;
using ChronoFlow.Client.Common.Processing.Search;

namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public sealed class EmployeeViewModel : MainDataViewModel
{
    [IncludeOnSearch]
    public EmployeeNameViewModel Name { get; init; } = EmployeeNameViewModel.Empty();

    [IncludeOnSearch]
    public AddressViewModel Address { get; init; } = AddressViewModel.Empty();

    public string PersonnelNumber { get; set; } = string.Empty;
    public List<EmployeeEmailViewModel> Emails { get; set; } = [];
    public List<EmployeePhoneNumberViewModel> PhoneNumbers { get; set; } = [];
    public List<EmployeeRoleViewModel> EmployeeRoles { get; set; } = [];
    public DateTime? Birthday { get; set; }

    internal bool HasRole(RoleViewModel role)
    {
        return EmployeeRoles.Any(r => r.RoleId == role.Id);
    }

    internal void ToggleRole(RoleViewModel role)
    {
        var employeeRole = EmployeeRoles.FirstOrDefault(r => r.RoleId == role.Id);
        if (employeeRole != null)
            EmployeeRoles.Remove(employeeRole);
        else
            EmployeeRoles.Add(EmployeeRoleViewModel.Create(Id, role));
    }
}
