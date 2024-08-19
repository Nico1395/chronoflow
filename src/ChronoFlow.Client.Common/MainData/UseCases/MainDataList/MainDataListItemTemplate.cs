using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public abstract class MainDataListItemTemplate<TViewModel> : ContainerListItemTemplate<TViewModel>
    where TViewModel : class
{
    [Parameter, EditorRequired]
    public required MainDataListContext<TViewModel> Context { get; set; }

    [Parameter, EditorRequired]
    public required MainDataList<TViewModel> ParentList { get; set; }

    protected Task OnDeleteAsync()
    {
        return ParentList.DeleteItemAsync(Item);
    }
}
