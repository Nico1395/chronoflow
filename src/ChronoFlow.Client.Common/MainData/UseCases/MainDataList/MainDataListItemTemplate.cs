using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public abstract class MainDataListItemTemplate<TViewModel> : ComponentBase
    where TViewModel : class
{
    protected TViewModel Item => Object as TViewModel ?? throw new InvalidCastException();

    [Parameter, EditorRequired]
    public required object Object { get; set; }

    [Parameter, EditorRequired]
    public required MainDataListContext<TViewModel> Context { get; set; }

    [Parameter, EditorRequired]
    public required EventCallback<TViewModel> OnDelete { get; set; }

    protected Task InvokeOnDeleteAsync()
    {
        return OnDelete.InvokeAsync(Item);
    }
}
