using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Notifications;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public partial class MainDataForm<TViewModel> : ComponentBase
    where TViewModel : class
{
    private bool _isBusy;
    private TViewModel? _item;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IBrowserLogger BrowserLogger { get; set; } = null!;

    [Inject]
    private ILocalNotificationPublisher LocalNotificationPublisher { get; set; } = null!;

    [Inject]
    private IMainDataFormService<TViewModel> FormService { get; set; } = null!;

    [Parameter]
    public Guid? Id { get; set; }

    [Parameter, EditorRequired]
    public required string ReturnUri { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadItemAsync();
    }

    private async Task LoadItemAsync()
    {
        await SetBusyAsync(true);

        try
        {
            //if (Id == null)
            //    _item = await GetNewAsync();
            //else
            //    _item = await GetByIdAsync();

            ArgumentNullException.ThrowIfNull(_item, "The item should not be null here");
        }
        catch (Exception ex)
        {
            await BrowserLogger.LogAsync(ex.Message);
            NavigationManager.NavigateTo(ReturnUri);
        }

        await SetBusyAsync(false);
    }

    //private async Task<TViewModel> GetNewAsync()
    //{
    //    var result = await FormService.GetNewAsync();
    //}

    //private async Task<TViewModel> GetByIdAsync()
    //{

    //}

    private Task SetBusyAsync(bool busy)
    {
        _isBusy = busy;
        return Task.Run(StateHasChanged);
    }
}
