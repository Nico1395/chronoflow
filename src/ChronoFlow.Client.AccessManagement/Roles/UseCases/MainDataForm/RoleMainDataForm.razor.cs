using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Controls.Data;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataForm;

public partial class RoleMainDataForm : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private IRoleMainDataFormService FormService { get; set; } = null!;

    [Parameter]
    public string? RoleId { get; set; }

    private string GetTitle(MainDataFormContext<RoleViewModel> context)
    {
        if (context.IsNew)
            return Localizer["NewRole"];

        return Localizer["EditingEntry", context.Item.Name];
    }

    private List<ListSortOption<PermissionViewModel>> GetPermissionListSortOptions()
    {
        var nameAscending = new ListSortOption<PermissionViewModel>(Localizer["NameAscending"], p => p.Name, ListSortDirection.Ascending);
        var nameDescending = new ListSortOption<PermissionViewModel>(Localizer["NameDescending"], p => p.Name, ListSortDirection.Descending);

        return [nameAscending, nameDescending];
    }
}
