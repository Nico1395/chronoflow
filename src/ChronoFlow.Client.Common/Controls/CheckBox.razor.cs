using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls;

public partial class CheckBox : ComponentBase
{
    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter]
    public string? Label { get; set; }

    private Task ValueChangedAsync()
    {
        return ValueChanged.InvokeAsync(Value = !Value);
    }
}
