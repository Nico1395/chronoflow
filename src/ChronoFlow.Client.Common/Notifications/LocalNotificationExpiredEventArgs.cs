namespace ChronoFlow.Client.Common.Notifications;

internal sealed class LocalNotificationExpiredEventArgs(Guid id) : EventArgs
{
    public Guid NotificationId { get; init; } = id;
}
