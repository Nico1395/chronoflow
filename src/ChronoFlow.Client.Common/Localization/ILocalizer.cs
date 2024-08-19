using System.Collections;

namespace ChronoFlow.Client.Common.Localization;

/// <summary>
/// Service provides localization.
/// </summary>
public interface ILocalizer
{
    /// <summary>
    /// Localizes a specified <paramref name="key"/>.
    /// </summary>
    /// <remarks>
    /// Event if the <paramref name="key"/> can be nullable the returned value will always be <see cref="string.Empty"/> for null safety reasons.
    /// </remarks>
    /// <param name="key">Key to be localized.</param>
    /// <returns>Localized string if found.</returns>
    public string this[string? key] { get; }

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
    public string this[string? formattableKey, params object?[] args] { get; }
}
