using MediatR;

namespace ChronoFlow.Server.Common.Messaging.Domain;

public interface ICommand<out TResult> : IRequest<TResult>
{
}
