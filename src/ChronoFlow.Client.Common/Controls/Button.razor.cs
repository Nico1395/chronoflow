using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls;

public partial class Button : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public string Type { get; set; } = "button";

    [Parameter]
    public string? IconLeft { get; set; }

    [Parameter]
    public string? IconLeftSize { get; set; }

    [Parameter]
    public string? Tooltip { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
}
