using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Controls.Data;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataForm;

public partial class EmployeeMainDataForm : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private IEmployeeMainDataFormService FormService { get; set; } = null!;

    [Parameter]
    public string? EmployeeId { get; set; }

    private string GetTitle(MainDataFormContext<EmployeeViewModel> context)
    {
        if (context.IsNew)
            return Localizer["NewEmployee"];

        return Localizer["EditingEntry", context.Item.Name.ToString()];
    }

    private List<ListSortOption<RoleViewModel>> GetRoleListSortOptions()
    {
        var nameAscending = new ListSortOption<RoleViewModel>(Localizer["NameAscending"], p => p.Name, ListSortDirection.Ascending);
        var nameDescending = new ListSortOption<RoleViewModel>(Localizer["NameDescending"], p => p.Name, ListSortDirection.Descending);

        return [nameAscending, nameDescending];
    }

    private bool ValidatePhoneNumber(List<EmployeePhoneNumberViewModel> phoneNumbers, EmployeePhoneNumberViewModel phoneNumber)
    {
        return true;
    }
}
