using ChronoFlow.Client.Common.Controls.Forms;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Data;

public partial class TextBox : FormControlComponentBase
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
    public int? MaxLength { get; set; }

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

        await OnInput.InvokeAsync(Value);
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task InvokeOnChangeAsync(ChangeEventArgs args)
    {
        if (args.Value as string != Value)
            Value = args.Value as string;

        await OnChange.InvokeAsync(Value);
    }
}
