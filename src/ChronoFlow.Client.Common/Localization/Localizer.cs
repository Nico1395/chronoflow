using ChronoFlow.Client.Common.Localization.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections;

namespace ChronoFlow.Client.Common.Localization;

/// <summary>
/// Service provides localization.
/// </summary>
internal sealed class Localizer : ILocalizer
{
    /// <summary>
    /// Cache of string localizers.
    /// </summary>
    private readonly List<IStringLocalizer> _stringLocalizers = [];

    /// <summary>
    /// Constructor creates <see cref="IStringLocalizer"/>s for all resource designer types registered in the <paramref name="options"/>.
    /// These string localizers will be iterated through when looking for a key.
    /// </summary>
    /// <param name="stringLocalizerFactory">Factory creating <see cref="IStringLocalizer"/>s.</param>
    /// <param name="options">Options containing resource designer types.</param>
    public Localizer(IStringLocalizerFactory stringLocalizerFactory, LocalizerOptions options)
    {
        foreach (var resource in options.GetResourceTypes())
        {
            var stringLocalizer = stringLocalizerFactory.Create(resource);
            _stringLocalizers.Add(stringLocalizer);
        }
    }

    public string this[string? key] => GetLocalizedString(key);
    public string this[string? formattableKey, params object?[] args] => GetFormattedLocalizedString(formattableKey, args);

    /// <summary>
    /// Localizes a specified <paramref name="key"/>.
    /// </summary>
    /// <remarks>
    /// Event if the <paramref name="key"/> can be nullable the returned value will always be <see cref="string.Empty"/> for null safety reasons.
    /// </remarks>
    /// <param name="key">Key to be localized.</param>
    /// <returns>Localized string if found.</returns>
    private string GetLocalizedString(string? key)
    {
        if (key == null)
            return string.Empty;

        foreach (var stringLocalizer in _stringLocalizers)
        {
            var localizedString = stringLocalizer[key];

            if (!localizedString.ResourceNotFound)
                return localizedString.Value;
        }

        return key;
    }

    /// <summary>
    /// Localizes a <paramref name="formattableKey"/> using <paramref name="args"/> for formatting.
    /// </summary>
    /// <remarks>
    /// The <paramref name="args"/> are being shallowly flattened. If an argument is some kind of <see cref="IEnumerable"/> it will be
    /// flattened. However if one of those arguments is an <see cref="IEnumerable"/> as well it will be left as it is.
    /// </remarks>
    /// <param name="formattableKey">Key to be localized.</param>
    /// <param name="args">Formatting arguments.</param>
    /// <returns>Localized and formatted string if found.</returns>
    private string GetFormattedLocalizedString(string? formattableKey, params object?[] args)
    {
        if (formattableKey == null)
            return string.Empty;

        var localizedString = GetLocalizedString(formattableKey);

        if (localizedString == null)
            return formattableKey;

        var flattenedArguments = GetFlattenedArguments(args);
        return string.Format(localizedString, flattenedArguments);
    }

    /// <summary>
    /// Shallowly flattenes the specified <paramref name="arguments"/>.
    /// </summary>
    /// <param name="arguments">Arguments to be flattened.</param>
    /// <returns>Flattened arguments.</returns>
    private object?[] GetFlattenedArguments(object?[] arguments)
    {
        var flattenedArguments = new List<object?>();

        foreach (var argument in arguments)
        {
            if (argument is IEnumerable enumerable && argument is not string)
            {
                foreach (var subItem in enumerable)
                    flattenedArguments.Add(subItem);
            }
            else
            {
                flattenedArguments.Add(argument);
            }
        }

        return flattenedArguments.ToArray();
    }
}
