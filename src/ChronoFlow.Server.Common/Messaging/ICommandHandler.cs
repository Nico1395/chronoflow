using ChronoFlow.Server.Common.Messaging.Domain;
using MediatR;

namespace ChronoFlow.Server.Common.Messaging;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
