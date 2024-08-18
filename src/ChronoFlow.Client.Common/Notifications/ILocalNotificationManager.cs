namespace ChronoFlow.Client.Common.Notifications;

public interface ILocalNotificationManager
{
    public void SubscribeNotificationsChanged(EventHandler notificationsChangedEventHandler);
    public void UnsubscribeNotificationsChanged(EventHandler notificationsChangedEventHandler);
    public List<LocalNotificationViewModel> GetActiveNotifications();
    public void Push(LocalNotificationViewModel notification);
    public void Remove(Guid notificationId);
}
