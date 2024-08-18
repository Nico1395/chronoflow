namespace ChronoFlow.Client.Common.Notifications;

public sealed class LocalNotificationViewModel
{
    private LocalNotificationViewModel()
    {
    }

    public Guid Id { get; init; }
    public LocalNotificationType Type { get; init; }
    public required string Message { get; init; }

    internal static LocalNotificationViewModel CreateSuccess(string message)
    {
        return new LocalNotificationViewModel()
        {
            Id = Guid.NewGuid(),
            Type = LocalNotificationType.Success,
            Message = message,
        };
    }

    internal static LocalNotificationViewModel CreateWarning(string message)
    {
        return new LocalNotificationViewModel()
        {
            Id = Guid.NewGuid(),
            Type = LocalNotificationType.Warning,
            Message = message,
        };
    }

    internal static LocalNotificationViewModel CreateInfo(string message)
    {
        return new LocalNotificationViewModel()
        {
            Id = Guid.NewGuid(),
            Type = LocalNotificationType.Info,
            Message = message,
        };
    }

    internal static LocalNotificationViewModel CreateError(string message)
    {
        return new LocalNotificationViewModel()
        {
            Id = Guid.NewGuid(),
            Type = LocalNotificationType.Error,
            Message = message,
        };
    }
}


