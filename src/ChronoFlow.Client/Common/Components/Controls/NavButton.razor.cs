using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Components.Controls;

public partial class NavButton : ControlComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Href { get; set; }

    [Parameter]
    public string? Width { get; set; }

    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string? IconLeft { get; set; }

    [Parameter]
    public string? IconLeftSize { get; set; }

    [Parameter]
    public string? IconRight { get; set; }

    [Parameter]
    public string? IconRightSize { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }

    private string? GetClasses()
    {
        return Css.CombineClasses(
            "c-nav-button",
            Css.GetClass(Active, "active"));
    }

    private string? GetStyling()
    {
        return Css.CombineAttributes(
            Css.GetStyle("width", Width, "fit-content"));
    }
}
