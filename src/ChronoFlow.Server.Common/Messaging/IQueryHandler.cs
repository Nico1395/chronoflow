using ChronoFlow.Server.Common.Messaging.Domain;
using MediatR;

namespace ChronoFlow.Server.Common.Messaging;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
