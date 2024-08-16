using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Server.Common.Messaging;

public interface IMediator
{
    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        where TResult : Result;

    public Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        where TResult : Result;

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}
