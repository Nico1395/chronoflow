using ChronoFlow.Client.Common.Controls.Forms;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls;

public partial class TextArea : FormControlComponentBase
{
    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<string?> OnInput { get; set; }

    [Parameter]
    public EventCallback<string?> OnChange { get; set; }

    [Parameter]
    public int? MaxLength { get; set; }

    [Parameter]
    public TextAreaResizeMode ResizeMode { get; set; } = TextAreaResizeMode.Vertical;

    private string GetClasses()
    {
        var resize = ResizeMode switch
        {
            TextAreaResizeMode.Both => "resize-both",
            TextAreaResizeMode.Horizontal => "resize-horizontal",
            _ or TextAreaResizeMode.Vertical => "resize-vertical",
        };
        var invalid = IsValid ? null : "invalid";

        return $"c-text-area {resize} {invalid}";
    }

    private string GetStyles()
    {
        var width = Width != null ? $"width:{Width};" : null;
        return $"{width}";
    }

    private async Task OnInputAsync(ChangeEventArgs args)
    {
        Value = args.Value as string;

        await OnInput.InvokeAsync(Value);
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task OnChangeAsync(ChangeEventArgs args)
    {
        if (args.Value as string != Value)
            Value = args.Value as string;

        await OnChange.InvokeAsync(Value);
    }
}
