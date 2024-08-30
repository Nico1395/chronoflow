using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Controls.Forms;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.Entities;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;
using ChronoFlow.Client.Common.Notifications;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public partial class MainDataForm<TViewModel> : ComponentBase, IDisposable
    where TViewModel : class, IMainDataViewModel
{
    internal Guid? ItemId { get; private set; }
    internal bool IsNew { get; private set; }
    internal bool IsBusy { get; private set; }
    internal TViewModel? Item { get; private set; }
    internal MainDataFormContext<TViewModel>? Context { get; private set; }

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

    [Inject]
    private IMainDataFormControllerManager FormControllerManager { get; set; } = null!;

    [Parameter]
    public object? Id { get; set; }

    [Parameter, EditorRequired]
    public required string ReturnUri { get; set; }

    [Parameter, EditorRequired]
    public required Func<MainDataFormContext<TViewModel>, string> Title { get; set; }

    [Parameter]
    public required RenderFragment<MainDataFormContext<TViewModel>> ChildContent { get; set; }

    public void Dispose()
    {
        FormControllerManager.Unregister(this);
    }

    internal void Render()
    {
        StateHasChanged();
    }

    internal Task SetBusyAsync(bool busy)
    {
        IsBusy = busy;
        return Task.Run(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        FormControllerManager.Register(this);

        if (Id == null)
        {
            IsNew = true;
        }
        else if (Guid.TryParse(Id.ToString(), out var id) && id != Guid.Empty)
        {
            ItemId = id;
            IsNew = false;
        }

        await LoadItemAsync();
    }

    private async Task LoadItemAsync()
    {
        await SetBusyAsync(true);

        try
        {
            if (IsNew)
                Item = await GetNewAsync();
            else
                Item = await GetByIdAsync();

            ArgumentNullException.ThrowIfNull(Item, "The item should not be null here");
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
            NavigateBack();
        }

        await FormService.OnLoadedAsync();
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
            LocalNotificationPublisher.PublishInfo(Localizer["NotAuthorizedToCreateEntry"]);

        NavigateBack();
        return null;
    }

    private async Task<TViewModel?> GetByIdAsync()
    {
        if (!ItemId.HasValue)
            return null;

        var result = await FormService.GetByIdAsync(ItemId.Value);
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
        if (Item == null)
            return;

        await SetBusyAsync(true);

        try
        {
            if (IsNew)
                await AddAsync(Item);
            else
                await UpdateAsync(Item);
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
            LocalNotificationPublisher.PublishError(Localizer["AnUnhandledErrorOccurred"]);
            NavigateBack();
        }

        await FormService.OnSavedAsync();
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
        if (IsNew || Item == null)
            return null;

        var created = TimespanMessageCalculator.GetCreatedMessage(Item.Created);
        var edited = TimespanMessageCalculator.GetEditedMessage(Item.LastChanged);

        return string.Join(" • ", [created, edited]);
    }

    private MainDataFormContext<TViewModel> CreateNewContext(TViewModel item, FormContext formContext)
    {
        return new MainDataFormContext<TViewModel>(item, IsBusy, IsNew, formContext);
    }
}
