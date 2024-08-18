using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.Validation.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ChronoFlow.Client.Common.Validation;

internal sealed class ValidationLocalizer(ILocalizer _localizer) : IValidationLocalizer
{
    public string LocalizeValidationError(ValidationAttribute validationAttribute, string propertyName, object? propertyValue)
    {
        return _localizer[
            validationAttribute.GetValidationType().GetMessageKey(),
            _localizer[propertyName],
            propertyValue?.ToString() ?? "null",
            validationAttribute.GetAttributeValues()];
    }
}
