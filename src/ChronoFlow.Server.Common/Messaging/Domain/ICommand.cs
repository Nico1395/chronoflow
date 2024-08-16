using MediatR;

namespace ChronoFlow.Server.Common.Messaging.Domain;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
