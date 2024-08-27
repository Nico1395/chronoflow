using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.Entities;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Client.Common.Notifications;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public partial class MainDataForm<TViewModel> : ComponentBase
    where TViewModel : class, IMainDataViewModel
{
    private Guid? _itemId;
    private bool _isNew;
    private bool _isBusy;
    private TViewModel? _item;

    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IBrowserLogger BrowserLogger { get; set; } = null!;

    [Inject]
    private ILocalNotificationPublisher LocalNotificationPublisher { get; set; } = null!;

    [Inject]
    private ITimespanMessageCalculator TimespanMessageCalculator { get; set; } = null!;

    [Inject]
    private IMainDataFormService<TViewModel> FormService { get; set; } = null!;

    [Parameter]
    public object? Id { get; set; }

    [Parameter, EditorRequired]
    public required string ReturnUri { get; set; }

    [Parameter, EditorRequired]
    public required Func<MainDataFormContext<TViewModel>, string> Title { get; set; }

    [Parameter]
    public required RenderFragment<MainDataFormContext<TViewModel>> ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Id == null)
        {
            _isNew = true;
        }
        else if (Guid.TryParse(Id.ToString(), out var id) && id != Guid.Empty)
        {
            _itemId = id;
            _isNew = false;
        }

        await LoadItemAsync();
    }

    private async Task LoadItemAsync()
    {
        await SetBusyAsync(true);

        try
        {
            if (_isNew)
                _item = await GetNewAsync();
            else
                _item = await GetByIdAsync();

            ArgumentNullException.ThrowIfNull(_item, "The item should not be null here");
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
            NavigateBack();
        }

        await SetBusyAsync(false);
    }

    private async Task<TViewModel?> GetNewAsync()
    {
        var result = await FormService.GetNewAsync();
        if (result.Code == MainDataGetNewResultCode.Success)
            return result.Item;
        else if (result.Code == MainDataGetNewResultCode.Error)
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
        else if (result.Code == MainDataGetNewResultCode.NotAuthorized)
            LocalNotificationPublisher.PublishInfo(Localizer["MissingPermissionsToCreateAnEntry"]);

        NavigateBack();
        return null;
    }

    private async Task<TViewModel?> GetByIdAsync()
    {
        if (!_itemId.HasValue)
            return null;

        var result = await FormService.GetByIdAsync(_itemId.Value);
        if (result.Code == MainDataGetByIdResultCode.Success)
            return result.Item;
        else if (result.Code == MainDataGetByIdResultCode.Error)
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
        else if (result.Code == MainDataGetByIdResultCode.NotFound)
            LocalNotificationPublisher.PublishWarning(Localizer["EntryNotFound"]);
        else if (result.Code == MainDataGetByIdResultCode.NotAuthorized)
            LocalNotificationPublisher.PublishInfo(Localizer["NotAuthorizedToCreateEntry"]);

        NavigateBack();
        return null;
    }

    private async Task SaveAsync()
    {
        if (_item == null)
            return;

        await SetBusyAsync(true);

        try
        {
            if (_isNew)
                await AddAsync(_item);
            else
                await UpdateAsync(_item);
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
            NavigateBack();
        }

        await SetBusyAsync(false);
    }

    private async Task AddAsync(TViewModel item)
    {
        // TODO -> Validation messages from the validation errors from the server.

        var result = await FormService.AddAsync(item);
        if (result.Code == MainDataAddResultCode.Success)
        {
            NavigateBack();
        }
        else if (result.Code == MainDataAddResultCode.Error)
        {
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
        }
        else if (result.Code == MainDataAddResultCode.AlreadyExists)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["EntryAlreadyExists"]);
        }
        else if (result.Code == MainDataAddResultCode.ValidationErrors)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["InvalidForm"]);
        }
        else if (result.Code == MainDataAddResultCode.NotAuthorized)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["NotAuthorizedToCreateEntry"]);
        }
    }

    private async Task UpdateAsync(TViewModel item)
    {
        // TODO -> Validation messages from the validation errors from the server.

        var result = await FormService.UpdateAsync(item);
        if (result.Code == MainDataUpdateResultCode.Success)
        {
            NavigateBack();
        }
        else if (result.Code == MainDataUpdateResultCode.Error)
        {
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
        }
        else if (result.Code == MainDataUpdateResultCode.AlreadyExists)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["EntryAlreadyExists"]);
        }
        else if (result.Code == MainDataUpdateResultCode.NotFound)
        {
            LocalNotificationPublisher.PublishWarning(Localizer["EntryNotFound"]);
        }
        else if (result.Code == MainDataUpdateResultCode.ValidationErrors)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["InvalidForm"]);
        }
        else if (result.Code == MainDataUpdateResultCode.NotAuthorized)
        {
            LocalNotificationPublisher.PublishInfo(Localizer["NotAuthorizedToCreateEntry"]);
        }
    }

    private void NavigateBack()
    {
        NavigationManager.NavigateTo(ReturnUri);
    }

    private string? GetSubtitle()
    {
        if (_isNew || _item == null)
            return null;

        var created = TimespanMessageCalculator.GetCreatedMessage(_item.Created);
        var edited = TimespanMessageCalculator.GetEditedMessage(_item.LastChanged);

        return string.Join(" • ", [created, edited]);
    }

    private Task SetBusyAsync(bool busy)
    {
        _isBusy = busy;
        return Task.Run(StateHasChanged);
    }
}
