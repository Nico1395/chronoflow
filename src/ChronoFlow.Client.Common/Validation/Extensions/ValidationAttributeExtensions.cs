using System.ComponentModel.DataAnnotations;

namespace ChronoFlow.Client.Common.Validation.Extensions;

internal static class ValidationAttributeExtensions
{
    private static readonly Type _validationExtensionsType = typeof(ValidationAttributeExtensions);
    private static readonly string _getValuesMethodName = nameof(GetValues);

    internal static object[] GetAttributeValues(this ValidationAttribute attribute)
    {
        var attributeType = attribute.GetType();
        var getValuesMethod = _validationExtensionsType.GetMethod(_getValuesMethodName, types: [attributeType]);

        if (getValuesMethod != null && getValuesMethod.Invoke(null, [attribute]) is object[] attributeValues)
            return attributeValues;

        return [];
    }

    public static object[] GetValues(this RangeAttribute rangeAttribute)
    {
        return [rangeAttribute.Minimum, rangeAttribute.Maximum];
    }

    public static object[] GetValues(this StringLengthAttribute stringLengthAttribute)
    {
        return [stringLengthAttribute.MinimumLength, stringLengthAttribute.MaximumLength];
    }

    public static object[] GetValues(this MaxLengthAttribute maxLengthAttribute)
    {
        return [maxLengthAttribute.Length];
    }

    public static object[] GetValues(this MinLengthAttribute minLengthAttribute)
    {
        return [minLengthAttribute.Length];
    }

    public static object[] GetValues(this FileExtensionsAttribute fileExtensionsAttribute)
    {
        return [string.Join(", ", fileExtensionsAttribute.Extensions)];
    }

    internal static ValidationType GetValidationType(this ValidationAttribute validationAttribute)
    {
        return validationAttribute.GetType().GetValidationType();
    }

    internal static ValidationType GetValidationType(this Type validationAttributeType)
    {
        if (!validationAttributeType.IsAssignableTo(typeof(ValidationAttribute)))
            throw new InvalidOperationException($"This method is only invokable from types inheriting from {typeof(ValidationAttribute).Name}.");

        return validationAttributeType.Name switch
        {
            nameof(RequiredAttribute) => ValidationType.Required,
            nameof(StringLengthAttribute) => ValidationType.StringLength,
            nameof(MaxLengthAttribute) => ValidationType.MaxLength,
            nameof(MinLengthAttribute) => ValidationType.MinLength,
            nameof(RangeAttribute) => ValidationType.Range,
            nameof(UrlAttribute) => ValidationType.Url,
            nameof(EmailAddressAttribute) => ValidationType.EmailAddress,
            nameof(FileExtensionsAttribute) => ValidationType.FileExtensions,
            _ => ValidationType.Unhandled,
        };
    }
}
