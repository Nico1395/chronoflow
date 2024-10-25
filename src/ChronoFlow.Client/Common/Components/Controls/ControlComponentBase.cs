using ChronoFlow.Client.Common.Components.Controls.Forms;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Components.Controls;

public abstract class ControlComponentBase : ComponentBase, IControl
{
    internal bool Validating { get; set; }
    internal bool Valid { get; set; } = true;

    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public int? TabIndex { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    protected bool IsDisabled()
    {
        return Disabled || Validating;
    }
}
