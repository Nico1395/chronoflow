namespace ChronoFlow.Shared.Common.Messaging;

public sealed record ValidationError
{
    public string? FieldName { get; init; }
    public string? Message { get; init; }
    public ValidationErrorType Type { get; init; }

    public static ValidationError Create(string? fieldName, string? message)
    {
        return new ValidationError()
        {
            FieldName = fieldName,
            Message = message,
        };
    }

    public static ValidationError AlreadyExists(string? message = null)
    {
        return new ValidationError()
        {
            Message = message,
            Type = ValidationErrorType.AlreadyExists,
        };
    }
}
