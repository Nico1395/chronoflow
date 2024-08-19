using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Layouts;

public partial class Layout : LayoutComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;
}
