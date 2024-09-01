using ChronoFlow.Client.Common.MainData.Entities;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public partial class MainDataListItemTemplate<TViewModel> : ComponentBase
    where TViewModel : class, IMainDataViewModel
{
    private bool _confirmationPending = false;
    private TViewModel? _item;

    [Inject]
    private ITimespanMessageCalculator TimespanMessageCalculator { get; set; } = null!;

    [Parameter, EditorRequired]
    public required TViewModel Item { get; set; }

    [Parameter, EditorRequired]
    public required Func<TViewModel, string> ItemUri { get; set; }

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
    public List<string> BottomDetails { get; set; } = [];

    [Parameter]
    public List<string> TopDetails { get; set; } = [];

    protected override void OnParametersSet()
    {
        _item = Item;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (_item != Item)
        {
            _confirmationPending = false;
            _item = Item;
        }
    }

    private string GetTopDetails()
    {
        return string.Join(" • ", TopDetails.Where(s => !string.IsNullOrEmpty(s)));
    }

    private string GetBottomDetails()
    {
        List<string> descriptionDetails = [];
        var createdMessage = TimespanMessageCalculator.GetCreatedMessage(Item.Created);
        var editedMessage = TimespanMessageCalculator.GetEditedMessage(Item.LastChanged);

        if (createdMessage != null)
            descriptionDetails.Add(createdMessage);

        if (editedMessage != null)
            descriptionDetails.Add(editedMessage);

        var descriptionValues = descriptionDetails.Concat(BottomDetails);
        return string.Join(" • ", descriptionValues.Where(s => !string.IsNullOrEmpty(s)));
    }

    private void ToggleConfirmation()
    {
        _confirmationPending = !_confirmationPending;
    }
}
