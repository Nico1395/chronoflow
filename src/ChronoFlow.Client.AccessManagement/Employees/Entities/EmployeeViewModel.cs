using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Objects.Aggregates;
using ChronoFlow.Client.Common.Objects.ValueObjects;
using ChronoFlow.Client.Common.Processing.Search;

namespace ChronoFlow.Client.AccessManagement.Employees.Entities;

public sealed class EmployeeViewModel : MainDataAggregateViewModel
{
    [IncludeOnSearch]
    public EmployeeNameViewModel Name { get; init; } = EmployeeNameViewModel.Empty();

    [IncludeOnSearch]
    public AddressViewModel Address { get; init; } = AddressViewModel.Empty();

    public string PersonnelNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<EmployeeEmailViewModel> Emails { get; init; } = [];
    public List<EmployeePhoneNumberViewModel> PhoneNumbers { get; set; } = [];
    public List<RoleViewModel> Roles { get; set; } = [];
    public DateTime? Birthday { get; set; }
}
