using ChronoFlow.Client.Common.Controls.Forms;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls;

public partial class Textbox : FormControlComponentBase
{
    [Parameter]
    public string? Type { get; set; } = "text";
    
    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<string?> OnChange { get; set; }

    [Parameter]
    public EventCallback<string?> OnInput { get; set; }

    [Parameter]
    public string? Class { get; set; }
    
    [Parameter]
    public string? Tooltip { get; set; }

    [Parameter]
    public string? Id { get; set; }

    private string GetClasses()
    {
        var invalid = IsValid ? null : "invalid";
        return $"c-textbox {invalid} {Class}".Trim();
    }

    private string GetStyles()
    {
        var width = Width != null ? $"width:{Width};" : null;
        return $"{width}";
    }

    private async Task InvokeOnInputAsync(ChangeEventArgs args)
    {
        Value = args.Value as string;

        await ValueChanged.InvokeAsync(Value);
        await OnInput.InvokeAsync(Value);
    }

    private async Task InvokeOnChangeAsync(ChangeEventArgs args)
    {
        Value = args.Value as string;

        await ValueChanged.InvokeAsync(Value);
        await OnChange.InvokeAsync(Value);
    }
}
