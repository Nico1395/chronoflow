using Microsoft.AspNetCore.Components.Forms;

namespace ChronoFlow.Client.Common.Validation;

public sealed record ValidationError
{
    private ValidationError()
    {
    }

    public FieldIdentifier Identifier { get; init; }
    public required object? PropertyValue { get; init; }
    public required string Message { get; init; }
    public required ValidationType Type { get; init; }

    public static ValidationError Create(ValidationType type, FieldIdentifier fieldIdentity, object? propertyValue, string message)
    {
        return new ValidationError()
        {
            Type = type,
            Identifier = fieldIdentity,
            PropertyValue = propertyValue,
            Message = message,
        };
    }
}

