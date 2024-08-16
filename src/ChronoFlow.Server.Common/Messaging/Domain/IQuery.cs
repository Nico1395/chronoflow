using MediatR;

namespace ChronoFlow.Server.Common.Messaging.Domain;

public interface IQuery : IRequest
{
}

public interface IQuery<out TResult> : IRequest<TResult>
{
}
