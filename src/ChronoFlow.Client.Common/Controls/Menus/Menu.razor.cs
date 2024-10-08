﻿using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Menus;

public partial class Menu : ComponentBase
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Parameter]
    public string? HeaderClass { get; set; }

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter]
    public RenderFragment? NothingSelected { get; set; }

    [Parameter, EditorRequired]
    public required List<MenuCategory> MenuCategories { get; set; }

    [Parameter]
    public MenuItem? SelectedMenuItem { get; set; }

    [Parameter]
    public EventCallback<MenuItem?> SelectedMenuItemChanged { get; set; }

    private Task OnSelectAsync(MenuItem menuItem)
    {
        if (menuItem == SelectedMenuItem)
            SelectedMenuItem = null;
        else
            SelectedMenuItem = menuItem;

        return SelectedMenuItemChanged.InvokeAsync(SelectedMenuItem);
    }
}
