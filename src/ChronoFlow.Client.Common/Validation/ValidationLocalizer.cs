using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.Validation.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ChronoFlow.Client.Common.Validation;

internal sealed class ValidationLocalizer(ILocalizer _localizer) : IValidationLocalizer
{
    public string LocalizeValidationError(ValidationAttribute validationAttribute, string propertyName, object? propertyValue)
    {
        var key = validationAttribute.GetValidationType().GetMessageKey();
        var attributeValues = validationAttribute.GetAttributeValues();
        var localizedPropertyValue = _localizer[propertyName];

        return _localizer[
            key,
            localizedPropertyValue,
            propertyValue?.ToString() ?? "null",
            attributeValues];
    }
}
