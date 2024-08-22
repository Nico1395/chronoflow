namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;

public sealed class MainDataMenuItemBuilder<TComponent>
{
    private readonly MainDataMenuItem _menuItem = new() { Component = typeof(TComponent), };

    public MainDataMenuItemBuilder<TComponent> WithTitle(string title)
    {
        _menuItem.Title = title;
        return this;
    }

    public MainDataMenuItemBuilder<TComponent> WithUri(string uri)
    {
        _menuItem.Uri = uri;
        return this;
    }

    public MainDataMenuItemBuilder<TComponent> WithTooltip(string tooltip)
    {
        _menuItem.Tooltip = tooltip;
        return this;
    }

    public MainDataMenuItemBuilder<TComponent> WithIconLeft(string iconLeft)
    {
        _menuItem.IconLeft = iconLeft;
        return this;
    }

    public MainDataMenuItemBuilder<TComponent> WithIconRight(string iconRight)
    {
        _menuItem.IconRight = iconRight;
        return this;
    }

    internal MainDataMenuItem Build()
    {
        return _menuItem;
    }
}
