namespace ChronoFlow.Client.Common.Controls.Data;

public sealed record ListSortOption<TItem>(string Name, Func<TItem, object> Field, ListSortDirection Direction, bool IsDefault = false)
    where TItem : class;
