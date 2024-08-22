using ChronoFlow.Client.Common.Controls.Menus;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;

internal sealed record MainDataMenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public required Type Component { get; set; }
    public Dictionary<string, object?> Parameters { get; set; } = [];
    public string? Tooltip { get; set; }
    public string? IconLeft { get; set; }
    public string? IconRight { get; set; }

    public MenuItem ToMenuItem()
    {
        return new MenuItem()
        {
            Title = Title,
            Uri = Uri,
            IconLeft = IconLeft,
            IconRight = IconRight,
            Component = Component,
            Parameters = Parameters,
            Tooltip = Tooltip,
        };
    }
}
