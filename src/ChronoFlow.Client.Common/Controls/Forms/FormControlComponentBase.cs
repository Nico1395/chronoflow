using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Forms;

public abstract class FormControlComponentBase : ComponentBase
{
    internal bool IsValid { get; set; } = true;
    internal bool IsValidating { get; set; }

    [CascadingParameter]
    public Field? Field { get; set; }

    [Parameter]
    public string? Width { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public string? TabIndex { get; set; }

    internal void SetWidth(string width)
    {
        Width = width;
    }

    internal bool GetDisabled()
    {
        return Disabled || IsValidating;
    }

    protected override void OnInitialized()
    {
        Field?.Register(this);
    }
}
