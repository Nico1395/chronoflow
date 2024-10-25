using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Components.Controls.Forms;

public partial class Field : ComponentBase, IField
{
    internal IControl? Control { get; set; }
    public bool Validating { get; set; }
    public bool Valid { get; set; }

    [CascadingParameter]
    private Form? ParentForm { get; set; }

    void IField.Register(IControl control)
    {
        Control = control;
    }
}
