namespace ChronoFlow.Client.Common.Notifications;

public interface ILocalNotificationPublisher
{
    public void PublishSuccess(string message);
    public void PublishError(string message);
    public void PublishWarning(string message);
    public void PublishInfo(string message);
    public void Publish(LocalNotificationType notificationType, string message);
}
