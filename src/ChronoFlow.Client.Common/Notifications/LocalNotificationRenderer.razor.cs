using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Notifications;

public partial class LocalNotificationRenderer : ComponentBase, IDisposable
{
    [Inject]
    private ILocalNotificationManager LocalNotificationManager { get; set; } = null!;

    public void Dispose()
    {
        LocalNotificationManager.UnsubscribeNotificationsChanged(OnNotificationsChanged);
    }

    protected override void OnInitialized()
    {
        LocalNotificationManager.SubscribeNotificationsChanged(OnNotificationsChanged);
    }

    private void OnNotificationsChanged(object? sender, EventArgs _)
    {
        StateHasChanged();
    }

    private void OnRemoveNotification(Guid notificationId)
    {
        LocalNotificationManager.Remove(notificationId);
    }
}
