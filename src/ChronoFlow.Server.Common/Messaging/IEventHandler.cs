using MediatR;

namespace ChronoFlow.Server.Common.Messaging;

public interface IEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}
