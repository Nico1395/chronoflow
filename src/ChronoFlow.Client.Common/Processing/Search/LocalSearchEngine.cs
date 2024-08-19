using System.Collections;
using System.Reflection;

namespace ChronoFlow.Client.Common.Processing.Search;

internal sealed class LocalSearchEngine : ILocalSearchEngine
{
    private readonly Type _stringType = typeof(string);

    public IEnumerable<TItem> SearchItems<TItem>(IEnumerable<TItem> items, SearchDescriptor searchDescriptor)
    {
        items = items.ToList();
        var itemType = items.FirstOrDefault()?.GetType() ?? typeof(TItem);

        var searchTerm = searchDescriptor.SearchTerm;
        if (!(searchDescriptor.Options?.CaseSensitive ?? false))
            searchTerm = searchTerm?.ToLowerInvariant();

        return itemType.IsValueType
            ? SearchValueTypes(items, searchDescriptor.Options, searchTerm)
            : SearchReferenceTypes(itemType, items, searchDescriptor.Options, searchTerm);
    }

    private IEnumerable<TItem> SearchValueTypes<TItem>(IEnumerable<TItem> items, SearchOptions? options, string? searchTerm)
    {
        return items.Where(i => IsMatchingValue(i, options, searchTerm));
    }

    private IEnumerable<TItem> SearchReferenceTypes<TItem>(Type itemType, IEnumerable<TItem> items, SearchOptions? options, string? searchTerm)
    {
        var properties = GetSearchableProperties(itemType);
        if (properties.Length == 0)
            return items;

        return items.Where(i => ItemHasMatchingValue(i, properties, options, searchTerm));
    }

    private bool ItemHasMatchingValue(object? item, PropertyInfo[] properties, SearchOptions? options, string? searchTerm)
    {
        for (var index = 0; index < properties.Length; index++)
        {
            var property = properties[index];
            var propertyValue = property.GetValue(item);

            if (property.PropertyType.IsValueType || property.PropertyType == _stringType)
            {
                if (IsMatchingValue(propertyValue, options, searchTerm))
                    return true;
            }
            else
            {
                var childProperties = GetSearchableProperties(property.PropertyType);
                return ItemHasMatchingValue(propertyValue, childProperties, options, searchTerm);
            }
        }

        return false;
    }

    private bool IsMatchingValue(object? value, SearchOptions? options, string? searchTerm)
    {
        if (searchTerm == null)
            return true;

        var valueString = value?.ToString();
        if (valueString == null)
            return false;

        if (!(options?.CaseSensitive ?? false))
            valueString = valueString.ToLowerInvariant();

        return EvaluateValue(options?.WholeTerm ?? false, valueString, searchTerm);
    }

    private bool EvaluateValue(bool wholeTerm, string valueString, string searchTerm)
    {
        return wholeTerm ? valueString == searchTerm : valueString.Contains(searchTerm);
    }

    private PropertyInfo[] GetSearchableProperties(Type objectType)
    {
        return objectType.GetProperties()
            .Where(ShouldEvaluateProperty)
            .ToArray();
    }

    private bool ShouldEvaluateProperty(PropertyInfo property)
    {
        if (property.GetCustomAttribute<IgnoreOnSearchAttribute>() != null)
            return false;

        if (property.PropertyType.IsValueType || property.PropertyType == _stringType)
            return true;

        if (!property.PropertyType.IsValueType)
        {
            if (property.GetCustomAttribute<IncludeOnSearchAttribute>() != null)
                return !property.PropertyType.IsAssignableTo(typeof(IEnumerable));

            return false;
        }

        return true;
    }
}