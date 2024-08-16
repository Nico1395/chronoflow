using ChronoFlow.Server.Common.Messaging.Domain;
using MediatR;

namespace ChronoFlow.Server.Common.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
