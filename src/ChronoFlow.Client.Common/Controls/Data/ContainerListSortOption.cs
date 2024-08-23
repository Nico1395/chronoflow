namespace ChronoFlow.Client.Common.Controls.Data;

public sealed record ContainerListSortOption<TItem>(string Name, Func<TItem, object> Field, ContainerListSortDirection Direction)
    where TItem : class;
