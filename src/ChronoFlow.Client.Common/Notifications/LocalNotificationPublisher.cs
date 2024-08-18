namespace ChronoFlow.Client.Common.Notifications;

internal sealed class LocalNotificationPublisher : ILocalNotificationPublisher
{
    public void Publish(LocalNotificationType notificationType, string message)
    {
        throw new NotImplementedException();
    }

    public void PublishError(string message)
    {
        throw new NotImplementedException();
    }

    public void PublishInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void PublishSuccess(string message)
    {
        throw new NotImplementedException();
    }

    public void PublishWarning(string message)
    {
        throw new NotImplementedException();
    }
}
