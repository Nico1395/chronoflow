using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Notifications;

public partial class LocalNotification : ComponentBase
{
    [Parameter, EditorRequired]
    public required LocalNotificationViewModel Notification { get; set; }

    [Parameter]
    public EventCallback<Guid> OnRemove { get; set; }

    private string GetClasses()
    {
        return $"c-notification {Notification.Type.ToClass()}";
    }

    private Task OnClickAsync()
    {
        return OnRemove.InvokeAsync(Notification.Id);
    }
}
