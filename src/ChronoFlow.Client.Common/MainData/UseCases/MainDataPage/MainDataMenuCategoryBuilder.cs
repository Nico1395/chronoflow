using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

public sealed class MainDataMenuCategoryBuilder
{
    private readonly MainDataMenuCategory _menuCategory = new();

    public MainDataMenuCategoryBuilder WithTitle(string title)
    {
        _menuCategory.Title = title;
        return this;
    }

    public MainDataMenuCategoryBuilder AddItem<TComponent>(Action<MainDataMenuItemBuilder<TComponent>> builderAction)
        where TComponent : ComponentBase
    {
        var builder = new MainDataMenuItemBuilder<TComponent>();
        builderAction.Invoke(builder);

        _menuCategory.Items.Add(builder.Build());
        return this;
    }

    internal MainDataMenuCategory Build()
    {
        return _menuCategory;
    }
}
