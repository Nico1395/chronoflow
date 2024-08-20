using ChronoFlow.Client.Common.Controls.Menus;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

internal sealed record MainDataMenuCategory
{
    public string Title { get; set; } = string.Empty;
    public List<MainDataMenuItem> Items { get; set; } = [];

    public MenuCategory ToMenuCategory()
    {
        return new MenuCategory()
        {
            Title = Title,
            Items = Items.Select(i => i.ToMenuItem()).ToList(),
        };
    }
}
