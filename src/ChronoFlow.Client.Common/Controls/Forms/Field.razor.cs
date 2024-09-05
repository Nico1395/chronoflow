using System.Linq.Expressions;
using ChronoFlow.Client.Common.Validation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ChronoFlow.Client.Common.Controls.Forms;

public partial class Field : ComponentBase
{
    private bool _displayMessages;

    internal FieldIdentifier Identifier { get; set; }
    internal List<ValidationError> ValidationErrors { get; set; } = [];
    internal FormControlComponentBase? Control { get; set; }
    internal bool IsValidating { get; set; }

    [CascadingParameter]
    internal Form? ParentForm { get; set; }

    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string? Tooltip { get; set; }

    [Parameter]
    public string? Width { get; set; }

    [Parameter]
    public Expression<Func<object>>? For { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    internal void SetValidationErrors(List<ValidationError> validationErrors)
    {
        ValidationErrors.AddRange(validationErrors);
        if (Control != null)
            Control.IsValid = ValidationErrors.Count == 0;
    }

    internal void ClearValidationErrors()
    {
        ValidationErrors.Clear();
        if (Control != null)
            Control.IsValid = true;
    }

    internal void Register(FormControlComponentBase control)
    {
        Control = control;

        if (Width != null)
            Control?.SetWidth(Width);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        if (For != null)
            Identifier = FieldIdentifier.Create(For);

        RequestValidationState();
        StateHasChanged();
    }

    private void RequestValidationState()
    {
        if (ParentForm == null)
            return;

        ParentForm.Register(this);
        _displayMessages = ParentForm.MessagePlacement == FormMessagePlacement.OnFields;

        var (IsValid, Error) = ParentForm.IsValidField(this);
        if (Control != null)
            Control.IsValid = IsValid;

        if (Error != null)
            ValidationErrors.Add(Error);
    }

    private string GetStyles()
    {
        var width = Width != null ? $"width:{Width};" : null;
        return $"{width}";
    }
}
