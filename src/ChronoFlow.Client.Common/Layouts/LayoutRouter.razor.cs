using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Layouts;

public partial class LayoutRouter : ComponentBase
{
    [Inject]
    private LayoutRouterOptions Options { get; set; } = null!;
}
