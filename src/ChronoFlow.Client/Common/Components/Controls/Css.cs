namespace ChronoFlow.Client.Common.Components.Controls;

public static class Css
{
    public static string? GetStyle(string attribute, string? value, string? @default = null)
    {
        return $"{attribute}:{value ?? @default};";
    }

    public static string? CombineAttributes(params string?[] attributes)
    {
        if (attributes.Length == 0)
            return null;

        return string.Join("", attributes);
    }

    public static string? GetClass(bool condition, string onTrue, string? onFalse = null)
    {
        return condition ? onTrue : onFalse;
    }

    public static string? CombineClasses(params string?[] classes)
    {
        if (!classes.Where(c => c != null).Any())
            return null;

        return string.Join(" ", classes);
    }

    public static string? GetBootstrapIcon(string? specializedIconClass)
    {
        if (specializedIconClass == null)
            return null;

        return CombineClasses("bi", $"bi-{specializedIconClass}");
    }

    public static string? GetBootstrapIconSize(string? iconSize)
    {
        return GetStyle("font-size", iconSize, "15px");
    }
}
