using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Data;

public abstract class ListItemComponentBase<TItem> : ComponentBase
    where TItem : class
{
    protected TItem Item => Object as TItem ?? throw new InvalidCastException();

    [Parameter, EditorRequired]
    public required object Object { get; set; }
}
