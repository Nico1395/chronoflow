using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

public sealed class MainDataMenuConfiguration
{
    internal List<MainDataMenuItem> MainDataMenuItems { get; } = [];

    public MainDataMenuConfiguration AddDomainObjectList<TComponent>(string key, string title, string category, string uri, string? tooltip = null)
        where TComponent : ComponentBase
    {
        var menuItem = new MainDataMenuItem()
        {
            Category = category,
            Key = key,
            Title = title,
            Uri = uri,
            Tooltip = tooltip,
            Component = typeof(TComponent),
        };

        MainDataMenuItems.Add(menuItem);
        return this;
    }
}
