using ChronoFlow.Shared.Common.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ChronoFlow.Server.Common.Messaging.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResult>(IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResult>
    where TRequest : notnull
    where TResult : Result
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var validators = _validators.ToList();
        if (validators.Count == 0)
            return await next.Invoke();

        var validationResults = new List<ValidationResult>();
        foreach (var validator in validators)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult != null && !validationResult.IsValid)
                validationResults.Add(validationResult);
        }

        var validationFailures = validationResults.SelectMany(v => v.Errors).ToList();
        if (validationFailures.Count > 0)
        {
            var validationErrors = validationFailures.Select(v => ValidationError.Create(v.PropertyName, v.ErrorMessage)).ToList();
            return (TResult)Result.Invalid(validationErrors);
        }

        return await next.Invoke();
    }
}
