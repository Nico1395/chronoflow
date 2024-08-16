using MediatR;

namespace ChronoFlow.Server.Common.Messaging;

public interface ICommand<out TResult> : IRequest<TResult>
{
}
