﻿using Microsoft.AspNetCore.Components;
using System.Net;

namespace ChronoFlow.Client.Common.Controls;

public partial class NavButton : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public string? Href { get; set; }

    [Parameter]
    public string? IconLeft { get; set; }

    [Parameter]
    public string? IconLeftSize { get; set; }

    [Parameter]
    public string? IconRight { get; set; }

    [Parameter]
    public string? IconRightSize { get; set; }

    [Parameter]
    public string? Tooltip { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Width { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }

    private string GetStyles()
    {
        var width = Width != null ? $"width:{Width};" : null;
        return $"{width}";
    }

    private string GetClasses()
    {
        var disabled = Disabled ? "disabled" : null;
        return $"c-nav-button {disabled} {Class}";
    }

    private string? GetHref()
    {
        return !Disabled ? Href : null;
    }
}
