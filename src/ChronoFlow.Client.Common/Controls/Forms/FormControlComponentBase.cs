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

    internal void SetWidth(string width)
    {
        Width = width;
    }

    protected override void OnInitialized()
    {
        Field?.Register(this);
    }
}
