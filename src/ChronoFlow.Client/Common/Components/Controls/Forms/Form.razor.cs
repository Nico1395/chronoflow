using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Components.Controls.Forms;

public partial class Form : ComponentBase, IForm
{
    private readonly List<IField> _fields = [];

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    void IForm.Register(IField field)
    {
        if (!_fields.Contains(field))
            _fields.Add(field);
    }
}
