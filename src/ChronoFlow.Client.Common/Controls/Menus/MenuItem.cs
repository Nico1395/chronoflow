namespace ChronoFlow.Client.Common.Controls.Menus;

public sealed record MenuItem
{
    public required string Title { get; init; }
    public required string Uri { get; init; }
    public required Type Component { get; init; }
    public Dictionary<string, object?> Parameters { get; init; } = [];
    public string? Tooltip { get; init; }
    public string? IconLeft { get; init; }
    public string? IconRight { get; init; }
}
