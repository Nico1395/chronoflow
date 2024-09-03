using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Data;

public partial class CheckList<TItem> : ListComponentBase<TItem>
    where TItem : class
{
    protected override bool ScrollToTopOnPageSelection => false;

    [Parameter]
    public EventCallback<TItem> OnToggle { get; set; }

    [Parameter, EditorRequired]
    public required Func<TItem, bool> CheckCondition { get; set; }

    private Task OnToggleAsync(TItem item)
    {
        return OnToggle.InvokeAsync(item);
    }
}