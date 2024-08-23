using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases;

public partial class RoleMainDataList : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    private string GetHeader(int roleCount)
    {
        var title = roleCount == 1 ? Localizer["Role"] : Localizer["Roles"];
        return $"{roleCount} {title}";
    }
}
