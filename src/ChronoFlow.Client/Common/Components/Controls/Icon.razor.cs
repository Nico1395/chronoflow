using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Components.Controls;

public partial class Icon : ComponentBase
{
    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Size { get; set; }
}
