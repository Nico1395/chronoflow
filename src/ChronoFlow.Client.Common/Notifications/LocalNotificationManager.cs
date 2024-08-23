namespace ChronoFlow.Client.Common.Notifications;

internal sealed class LocalNotificationManager : ILocalNotificationManager, IDisposable
{
    private readonly List<LocalNotificationPublishing> _publishedNotifications = [];

    private event EventHandler? NotificationsChangedEvent;

    public void Dispose()
    {
        _publishedNotifications.ForEach(p => p.Dispose());
    }

    public List<LocalNotificationViewModel> GetActiveNotifications()
    {
        return _publishedNotifications.OrderBy(p => p.Published).Select(p => p.Notification).ToList();
    }

    public void Push(LocalNotificationViewModel notification)
    {
        var publishing = LocalNotificationPublishing.Create(notification, OnNotificationExpired);
        _publishedNotifications.Add(publishing);
        NotificationsChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void Remove(Guid notificationId)
    {
        var publishing = _publishedNotifications.FirstOrDefault(p => p.Notification.Id == notificationId);
        if (publishing == null)
            return;

        publishing.Unsubscribe(OnNotificationExpired);
        publishing.Dispose();
        _publishedNotifications.Remove(publishing);
        NotificationsChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SubscribeNotificationsChanged(EventHandler notificationsChangedEventHandler)
    {
        NotificationsChangedEvent += notificationsChangedEventHandler;
    }

    public void UnsubscribeNotificationsChanged(EventHandler notificationsChangedEventHandler)
    {
        NotificationsChangedEvent -= notificationsChangedEventHandler;
    }

    private void OnNotificationExpired(object? sender, LocalNotificationExpiredEventArgs args)
    {
        Remove(args.NotificationId);
    }
}
