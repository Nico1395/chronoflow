using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Server.Common.Messaging;

internal sealed class Mediator(MediatR.IMediator _mediator) : IMediator
{
    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        where TResult : Result
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        where TResult : Result
    {
        return _mediator.Send(query, cancellationToken);
    }

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        return _mediator.Publish(@event, cancellationToken);
    }
}
