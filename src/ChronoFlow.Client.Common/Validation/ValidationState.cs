namespace ChronoFlow.Client.Common.Validation;

public sealed record ValidationState
{
    private ValidationState()
    {
    }

    public required object? Value { get; init; }
    public required bool IsValid { get; init; }
    public required List<ValidationError> Errors { get; init; }

    internal static ValidationState Create(object? value, List<ValidationError> errors)
    {
        return new ValidationState()
        {
            Value = value,
            IsValid = errors.Count == 0,
            Errors = errors,
        };
    }
}
