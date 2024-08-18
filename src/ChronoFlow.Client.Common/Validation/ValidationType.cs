namespace ChronoFlow.Client.Common.Validation;

public enum ValidationType
{
    Unhandled,
    Required,
    StringLength,
    MaxLength,
    MinLength,
    Range,
    EmailAddress,
    Url,
    FileExtensions,
}
