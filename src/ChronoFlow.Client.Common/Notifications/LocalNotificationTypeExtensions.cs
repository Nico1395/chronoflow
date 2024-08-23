namespace ChronoFlow.Client.Common.Notifications;

internal static class LocalNotificationTypeExtensions
{
    internal static string ToIcon(this LocalNotificationType notificationType)
    {
        return notificationType switch
        {
            LocalNotificationType.Success => "bi bi-check2-circle",
            LocalNotificationType.Error => "bi bi-bug",
            LocalNotificationType.Warning => "bi bi-exclamation-circle",
            LocalNotificationType.Info => "bi bi-info-circle",
            _ => throw new Exception(),
        };
    }

    internal static string ToClass(this LocalNotificationType notificationType)
    {
        return notificationType switch
        {
            LocalNotificationType.Success => "success",
            LocalNotificationType.Error => "error",
            LocalNotificationType.Warning => "warning",
            LocalNotificationType.Info => "info",
            _ => throw new Exception(),
        };
    }
}
