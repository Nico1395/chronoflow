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
}
