namespace ChronoFlow.Client.Common.Controls.Menus;

public sealed record MenuCategory
{
    public string Title { get; set; } = string.Empty;
    public List<MenuItem> Items { get; set; } = [];
}
