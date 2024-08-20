using ChronoFlow.Client.Common.Controls.Menus;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

internal sealed record MainDataMenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public required Type Component { get; set; }
    public string? Tooltip { get; set; }
    public string? IconLeft { get; set; }
    public string? IconRight { get; set; }

    public MenuItem ToMenuItem()
    {
        return new MenuItem()
        {
            Title = Title,
            Uri = Uri,
            Component = Component,
            Tooltip = Tooltip,
            IconLeft = IconLeft,
            IconRight = IconRight,
        };
    }
}
