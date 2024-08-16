using ChronoFlow.Shared.Common.Messaging;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ChronoFlow.Server.Common.Messaging.Behaviors;

// This handler should be replaced once the system monitoring exception handler is used
internal class GlobalExceptionHandler<TRequest, TResponse, TException>(ILogger<GlobalExceptionHandler<TRequest, TResponse, TException>> _logger) : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
    where TException : Exception
{
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, $"An exception occurred when handling request '{typeof(TRequest)}'.");

        state.SetHandled(null!);
        return Task.CompletedTask;
    }
}