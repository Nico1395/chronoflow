using ChronoFlow.Client.Common.Validation.Extensions;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChronoFlow.Client.Common.Validation;

/// <summary>
/// Service validates a specified models property values and validation attributes.
/// </summary>
internal sealed class ValidationService(IValidationLocalizer _validationLocalizer) : IValidationService
{
    /// <summary>
    /// Service validates a specified models property values and validation attributes.
    /// </summary>
    /// <param name="viewModel">View model to be validated.</param>
    /// <param name="editContext">Edit context of the form.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>State of the validation process.</returns>
    public ValidationState Validate(object viewModel, EditContext editContext, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        var validationContext = new ValidationContext(editContext);
        var validationErrors = new List<ValidationError>();

        ValidateObject(viewModel, validationContext, validationErrors, cancellationToken);
        return ValidationState.Create(viewModel, validationErrors);
    }

    /// <summary>
    /// Method validates an object.
    /// </summary>
    /// <param name="object">Object to be validated.</param>
    /// <param name="validationContext">Context of the validation process.</param>
    /// <param name="validationErrors">List of validation errors that is being filled up with validation errors as they arise.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    private void ValidateObject(object? @object, ValidationContext validationContext, List<ValidationError> validationErrors, CancellationToken cancellationToken = default)
    {
        if (@object == null)
            return;

        var objectType = @object.GetType();
        var propertiesToBeValidated = objectType.GetProperties().Where(p =>
        {
            var hasValidationAttributes = p.GetCustomAttributes<ValidationAttribute>().Any();
            var isChildObject = !p.PropertyType.IsValueType && !p.PropertyType.IsAssignableTo(typeof(IEnumerable));
            var ignoreChildObject = p.GetCustomAttribute<IgnoreOnValidationAttribute>() != null;

            return hasValidationAttributes || isChildObject && !ignoreChildObject;
        });

        foreach (var property in propertiesToBeValidated)
            ValidateProperty(@object, property, validationErrors, validationContext, cancellationToken);
    }

    /// <summary>
    /// Method validates a specified <paramref name="property"/> on the specified <paramref name="object"/>.
    /// </summary>
    /// <param name="object">View model instance whose <paramref name="property"/> is to be validated.</param>
    /// <param name="property">Property information of the property to be validated.</param>
    /// <param name="validationErrors">List of validation errors that is being filled up with validation errors as they arise.</param>
    /// <param name="validationContext">Context of the validation process.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    private void ValidateProperty(object @object, PropertyInfo property, List<ValidationError> validationErrors, ValidationContext validationContext, CancellationToken cancellationToken)
    {
        var propertyValue = property.GetValue(@object, null);
        if (property.PropertyType.IsValueType || property.PropertyType.IsAssignableTo(typeof(IEnumerable)))
        {
            var validationAttributes = property.GetCustomAttributes<ValidationAttribute>();
            foreach (var attribute in validationAttributes)
                ValidateAttribute(@object, property.Name, propertyValue, attribute, validationErrors, cancellationToken);
        }
        else
        {
            ValidateObject(
                propertyValue,
                validationContext,
                validationErrors,
                cancellationToken);
        }
    }

    /// <summary>
    /// Method validates a specified <paramref name="attribute"/> using the <paramref name="propertyValue"/> of the <paramref name="object"/>
    /// property with the name <paramref name="propertyName"/>.
    /// </summary>
    /// <param name="object">View model instance whose properties <paramref name="attribute"/> is to be validated.</param>
    /// <param name="propertyName">Name of the property with the <paramref name="attribute"/>.</param>
    /// <param name="propertyValue">Value of the property used for validation.</param>
    /// <param name="attribute">Attribute validating the <paramref name="propertyValue"/>.</param>
    /// <param name="validationErrors">List of validation errors that is being filled up with validation errors as they arise.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    private void ValidateAttribute(object @object, string propertyName, object? propertyValue, ValidationAttribute attribute, List<ValidationError> validationErrors, CancellationToken cancellationToken)
    {
        var isValid = attribute.IsValid(propertyValue);
        if (isValid)
            return;

        validationErrors.Add(ValidationError.Create(
            attribute.GetValidationType(),
            new FieldIdentifier(@object, propertyName),
            propertyValue,
            _validationLocalizer.LocalizeValidationError(attribute, propertyName, propertyValue)));
    }
}
