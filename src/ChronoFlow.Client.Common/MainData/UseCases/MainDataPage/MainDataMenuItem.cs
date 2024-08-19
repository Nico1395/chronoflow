using ChronoFlow.Client.Common.Controls.Menus;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

internal sealed class MainDataMenuItem
{
    public required string Key { get; set; }
    public required string Title { get; set; }
    public required string Category { get; set; }
    public required string Uri { get; set; }
    public required Type Component { get; set; }
    public string? Tooltip { get; set; }
    public string? IconLeft { get; init; }
    public string? IconRight { get; init; }

    public MenuItem ToMenuItem()
    {
        return new MenuItem()
        {
            Key = Key,
            Title = Title,
            Uri = Uri,
            Category = Category,
            Component = Component,
            Tooltip = Tooltip,
        };
    }
}
