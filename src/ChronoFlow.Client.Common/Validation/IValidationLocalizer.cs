using System.ComponentModel.DataAnnotations;

namespace ChronoFlow.Client.Common.Validation;

public interface IValidationLocalizer
{
    public string LocalizeValidationError(ValidationAttribute validationAttribute, string propertyName, object? propertyValue);
}
