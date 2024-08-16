namespace ChronoFlow.Shared.Common.Messaging;

public record Result
{
    public required ResponseCode Code { get; init; }
    public IReadOnlyList<ValidationError> ValidationErrors { get; init; } = [];

    public static Result Invalid(List<ValidationError> validationErrors)
    {
        return new Result()
        {
            Code = ResponseCode.ValidationErrors,
            ValidationErrors = validationErrors,
        };
    }

    public static Result<TData> Invalid<TData>(List<ValidationError> validationErrors)
    {
        return new Result<TData>()
        {
            Code = ResponseCode.ValidationErrors,
            ValidationErrors = validationErrors,
        };
    }
}

public sealed record Result<TData> : Result
{
    public TData? Data { get; init; }
}
