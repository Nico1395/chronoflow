using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataForm;

public partial class RoleMainDataForm : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Parameter]
    public string? RoleId { get; set; }

    private string GetTitle(MainDataFormContext<RoleViewModel> context)
    {
        if (context.IsNew)
            return Localizer["NewRole"];

        return Localizer["EditingEntry", context.Item.Name];
    }
}
