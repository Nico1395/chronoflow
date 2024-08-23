namespace ChronoFlow.Shared.Common.Messaging;

public sealed record ValidationError
{
    public required string FieldName { get; init; }
    public required string Message { get; init; }

    public static ValidationError Create(string fieldName, string message)
    {
        return new ValidationError()
        {
            FieldName = fieldName,
            Message = message,
        };
    }
}
