using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Controls.Data;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Client.Common.Notifications;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public partial class MainDataList<TViewModel> : ComponentBase
    where TViewModel : class
{
    private List<TViewModel> _items = [];
    private bool _busy;

    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private ILocalNotificationPublisher LocalNotificationPublisher { get; set; } = null!;

    [Inject]
    private IBrowserLogger BrowserLogger { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IMainDataListService<TViewModel> ListService { get; set; } = null!;

    [Parameter, EditorRequired]
    public required RenderFragment<MainDataListContext<TViewModel>> Header { get; set; }

    [Parameter, EditorRequired]
    public required Type ItemTemplate { get; set; }

    [Parameter]
    public string? ItemClass { get; set; }

    [Parameter]
    public List<ListSortOption<TViewModel>> SortOptions { get; set; } = [];

    [Parameter, EditorRequired]
    public required string NewUri { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await LoadItemsAsync();
    }

    private async Task LoadItemsAsync()
    {
        await SetBusyAsync(true);

        try
        {
            var result = await ListService.GetAllAsync();
            _items = EvaluateGetAllResult(result);
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
        }

        await SetBusyAsync(false);
    }

    private List<TViewModel> EvaluateGetAllResult(MainDataGetAllResult<TViewModel> result)
    {
        if (result.Code == MainDataGetAllResultCode.Success)
        {
            return result.Items ?? _items;
        }
        else if (result.Code == MainDataGetAllResultCode.Error)
        {
            LocalNotificationPublisher.PublishError(Localizer["AnUnknownErrorOccurred", result.Message ?? string.Empty]);
            return _items;
        }
        else if (result.Code == MainDataGetAllResultCode.NotAuthorized)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["MissingPermissionsToViewList"]);
            NavigationManager.NavigateTo("/");
            return [];
        }
        else
        {
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
            NavigationManager.NavigateTo("/");
            return [];
        }
    }

    internal async Task DeleteItemAsync(TViewModel item)
    {
        await SetBusyAsync(true);

        try
        {
            var result = await ListService.DeleteAsync(item);
            EvaluateDeleteResult(result);
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
        }

        await SetBusyAsync(false);
        await LoadItemsAsync();
    }

    private void EvaluateDeleteResult(MainDataDeleteResult result)
    {
        if (result.Code == MainDataDeleteResultCode.Success)
        {
            LocalNotificationPublisher.PublishSuccess(Localizer["EntryDeleted"]);
        }
        else if (result.Code == MainDataDeleteResultCode.Error)
        {
            LocalNotificationPublisher.PublishError(Localizer["AnUnknownErrorOccurred", result.Message ?? string.Empty]);
        }
        else if (result.Code == MainDataDeleteResultCode.NotFound)
        {
            LocalNotificationPublisher.PublishWarning(Localizer["EntryNotFound"]);
        }
        else if (result.Code == MainDataDeleteResultCode.NotAuthorized)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["MissingPermissionsToDeleteEntry"]);
        }
    }

    private void NavigateToNew()
    {
        NavigationManager.NavigateTo(NewUri);
    }

    private Task SetBusyAsync(bool busy)
    {
        _busy = busy;
        return Task.Run(StateHasChanged);
    }

    private string GetItemClasses()
    {
        return $"md-list-item {ItemClass}";
    }

    private List<TViewModel> GetProcessedItems()
    {
        return _items;
    }

    private Dictionary<string, object?> CreateTemplateParameters(TViewModel item, MainDataListContext<TViewModel> context)
    {
        return new Dictionary<string, object?>()
        {
            { nameof(MainDataListItemComponentBase<TViewModel>.Object), item },
            { nameof(MainDataListItemComponentBase<TViewModel>.Context), context },
            { nameof(MainDataListItemComponentBase<TViewModel>.ParentList), this },
        };
    }
}
