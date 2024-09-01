namespace ChronoFlow.Shared.Common.Objects.Extensions;

public static class StringExtensions
{
    public static string Shorten(this string? @string, uint maxLength)
    {
        if (@string == null)
            return string.Empty;

        if (@string.Length <= maxLength)
            return @string;

        return @string.Substring(0, (int)maxLength);
    }
}
