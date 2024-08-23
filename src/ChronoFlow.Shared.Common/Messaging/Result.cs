namespace ChronoFlow.Shared.Common.Messaging;

public record Result
{
    public required ResponseCode Code { get; init; }
    public string? Message { get; init; }
    public IReadOnlyList<ValidationError> ValidationErrors { get; init; } = [];

    public static Result Okay() => new() { Code = ResponseCode.Okay, };
    public static Result Error(string? message) => new() { Code = ResponseCode.Error, Message = message, };
    public static Result NotFound() => new() { Code = ResponseCode.NotFound, };
    public static Result AlreadyExists(List<ValidationError>? validationErrors = null) => new() { Code = ResponseCode.AlreadyExists, ValidationErrors = validationErrors ?? [] };
    public static Result Invalid(List<ValidationError> validationErrors)
    {
        return new Result()
        {
            Code = ResponseCode.ValidationErrors,
            ValidationErrors = validationErrors,
        };
    }

    public static Result<TData> Okay<TData>(TData data) => new() { Code = ResponseCode.Okay, Data = data, };
    public static Result<TData> Error<TData>(string? message) => new() { Code = ResponseCode.Error, Message = message, };
    public static Result<TData> NotFound<TData>() => new() { Code = ResponseCode.NotFound, };
    public static Result<TData> AlreadyExists<TData>(List<ValidationError>? validationErrors = null) => new() { Code = ResponseCode.AlreadyExists, ValidationErrors = validationErrors ?? [] };
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
