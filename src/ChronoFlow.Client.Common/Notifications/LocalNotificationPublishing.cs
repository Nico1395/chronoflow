namespace ChronoFlow.Client.Common.Notifications;

internal sealed class LocalNotificationPublishing(LocalNotificationViewModel notification) : IDisposable
{
    private readonly TimeSpan _displayDuration = TimeSpan.FromSeconds(3);
    private Timer? _internalTimer;
    private bool _firstCountdownInvoke = true;
    private event EventHandler<LocalNotificationExpiredEventArgs>? NotificationExpiredEvent;

    public LocalNotificationViewModel Notification { get; } = notification;
    public DateTime Published { get; } = DateTime.UtcNow;

    internal static LocalNotificationPublishing Create(LocalNotificationViewModel notification, EventHandler<LocalNotificationExpiredEventArgs> notificationExpiredEventHandler)
    {
        var publishing = new LocalNotificationPublishing(notification);
        publishing.Subscribe(notificationExpiredEventHandler);
        publishing.StartCountdown();

        return publishing;
    }

    public void Dispose()
    {
        if (_internalTimer == null)
            return;

        _internalTimer.Dispose();
    }

    internal void Unsubscribe(EventHandler<LocalNotificationExpiredEventArgs> notificationExpiredEventHandler)
    {
        NotificationExpiredEvent -= notificationExpiredEventHandler;
    }

    private void Subscribe(EventHandler<LocalNotificationExpiredEventArgs> notificationExpiredEventHandler)
    {
        NotificationExpiredEvent += notificationExpiredEventHandler;
    }

    private void StartCountdown()
    {
        _internalTimer = new Timer(OnCountdown, null, 0, (int)_displayDuration.TotalMilliseconds);
    }

    private void OnCountdown(object? sender)
    {
        if (_firstCountdownInvoke == true)
        {
            _firstCountdownInvoke = false;
            return;
        }

        Dispose();

        var eventArgs = new LocalNotificationExpiredEventArgs(Notification.Id);
        NotificationExpiredEvent?.Invoke(this, eventArgs);
    }
}
