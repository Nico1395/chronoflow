using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Forms;

public partial class FormSection : ComponentBase
{
    [Parameter, EditorRequired]
    public required string Title { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
