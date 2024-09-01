using ChronoFlow.Client.Common.Controls.Data;
using ChronoFlow.Client.Common.MainData.Entities;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public abstract class MainDataListItemComponentBase<TViewModel> : ListItemComponentBase<TViewModel>
    where TViewModel : class, IMainDataViewModel
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
