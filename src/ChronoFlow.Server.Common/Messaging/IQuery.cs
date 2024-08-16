using MediatR;

namespace ChronoFlow.Server.Common.Messaging;

public interface IQuery<out TResult> : IRequest<TResult>
{
}
