using ChronoFlow.Client.Common.MainData.Entities;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public partial class MainDataListItemTemplate<TViewModel> : ComponentBase
    where TViewModel : class, IMainDataViewModel
{
    [Inject]
    private ITimespanMessageCalculator TimespanMessageCalculator { get; set; } = null!;

    [Parameter, EditorRequired]
    public required TViewModel Item { get; set; }

    [Parameter, EditorRequired]
    public required Func<TViewModel, string> ItemUriFactory { get; set; }

    [Parameter, EditorRequired]
    public required Func<TViewModel, string> ItemTitle { get; set; }

    [Parameter, EditorRequired]
    public required MainDataListContext<TViewModel> Context { get; set; }

    [Parameter, EditorRequired]
    public EventCallback OnDelete { get; set; }

    [Parameter]
    public RenderFragment? Left { get; set; }

    [Parameter]
    public RenderFragment? Right { get; set; }

    [Parameter]
    public List<string> BottomRowValues { get; set; } = [];

    [Parameter]
    public List<string> TopRowValues { get; set; } = [];

    private string GetTopRow()
    {
        return string.Join(" • ", TopRowValues);
    }

    private string GetBottomRow()
    {
        List<string> descriptionDetails = [];
        var createdMessage = TimespanMessageCalculator.GetCreatedMessage(Item.Created);
        var editedMessage = TimespanMessageCalculator.GetEditedMessage(Item.LastChanged);

        if (createdMessage != null)
            descriptionDetails.Add(createdMessage);

        if (editedMessage != null)
            descriptionDetails.Add(editedMessage);

        var descriptionValues = descriptionDetails.Concat(BottomRowValues);
        return string.Join(" • ", descriptionValues);
    }
}
