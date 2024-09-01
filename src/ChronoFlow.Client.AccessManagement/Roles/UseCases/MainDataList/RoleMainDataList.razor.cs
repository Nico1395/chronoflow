using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Controls.Data;
using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataList;

public partial class RoleMainDataList : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    private string GetHeader(int roleCount)
    {
        var title = roleCount == 1 ? Localizer["Role"] : Localizer["Roles"];
        return $"{roleCount} {title}";
    }

    private List<ListSortOption<RoleViewModel>> GetSortOptions()
    {
        var nameAscending = new ListSortOption<RoleViewModel>(Localizer["NameAscending"], p => p.Name, ListSortDirection.Ascending, IsDefault: true);
        var nameDescending = new ListSortOption<RoleViewModel>(Localizer["NameDescending"], p => p.Name, ListSortDirection.Descending);

        return [nameAscending, nameDescending];
    }
}
