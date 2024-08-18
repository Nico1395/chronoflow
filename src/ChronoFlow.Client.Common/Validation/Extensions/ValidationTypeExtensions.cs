namespace ChronoFlow.Client.Common.Validation.Extensions;

internal static class ValidationTypeExtensions
{
    internal static string GetMessageKey(this ValidationType validationType)
    {
        return validationType switch
        {
            ValidationType.Required => "RequiredValidationErrorMessage",
            ValidationType.StringLength => "StringLengthValidationErrorMessage",
            ValidationType.MaxLength => "MaxLengthValidationErrorMessage",
            ValidationType.MinLength => "MinLengthValidationErrorMessage",
            ValidationType.Range => "RangeValidationErrorMessage",
            ValidationType.Url => "UrlValidationErrorMessage",
            ValidationType.EmailAddress => "EmailAddressValidationErrorMessage",
            ValidationType.FileExtensions => "FileExtensionsValidationErrorMessage",
            ValidationType.Unhandled or _ => "UnhandledValidationErrorMessage",
        };
    }
}
