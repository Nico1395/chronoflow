namespace ChronoFlow.Client.Common.Notifications;

internal sealed class LocalNotificationPublisher(ILocalNotificationManager _localNotificationManager) : ILocalNotificationPublisher
{
    public void PublishError(string message)
    {
        _localNotificationManager.Push(LocalNotificationViewModel.CreateError(message));
    }

    public void PublishInfo(string message)
    {
        _localNotificationManager.Push(LocalNotificationViewModel.CreateInfo(message));
    }

    public void PublishSuccess(string message)
    {
        _localNotificationManager.Push(LocalNotificationViewModel.CreateSuccess(message));
    }

    public void PublishWarning(string message)
    {
        _localNotificationManager.Push(LocalNotificationViewModel.CreateWarning(message));
    }

    public void Publish(LocalNotificationType notificationType, string message)
    {
        var notification = notificationType switch
        {
            LocalNotificationType.Error => LocalNotificationViewModel.CreateError(message),
            LocalNotificationType.Warning => LocalNotificationViewModel.CreateWarning(message),
            LocalNotificationType.Success => LocalNotificationViewModel.CreateSuccess(message),
            LocalNotificationType.Info => LocalNotificationViewModel.CreateInfo(message),
            _ => throw new ArgumentException(nameof(notificationType), "Notification type not yet handled."),
        };

        _localNotificationManager.Push(notification);
    }
}
