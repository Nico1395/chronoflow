using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.Common.Controls.Data;
using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataList;

public partial class EmployeeMainDataList : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    private string GetHeader(int employeeCount)
    {
        var title = employeeCount == 1 ? Localizer["Employee"] : Localizer["Employees"];
        return $"{employeeCount} {title}";
    }

    private List<ListSortOption<EmployeeViewModel>> GetSortOptions()
    {
        var personnelNumberAscending = new ListSortOption<EmployeeViewModel>(Localizer["PersonnelNumberAscending"], p => p.PersonnelNumber, ListSortDirection.Ascending, IsDefault: true);
        var personnelNumberDescending = new ListSortOption<EmployeeViewModel>(Localizer["PersonnelNumberDescending"], p => p.PersonnelNumber, ListSortDirection.Descending);
        var nameAscending = new ListSortOption<EmployeeViewModel>(Localizer["NameAscending"], p => p.Name, ListSortDirection.Ascending, IsDefault: true);
        var nameDescending = new ListSortOption<EmployeeViewModel>(Localizer["NameDescending"], p => p.Name, ListSortDirection.Descending);

        return [personnelNumberAscending, personnelNumberDescending, nameAscending, nameDescending];
    }
}
