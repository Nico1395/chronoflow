namespace ChronoFlow.Client.Common.Processing.Search;

public interface ILocalSearchEngine
{
    public IEnumerable<TItem> SearchItems<TItem>(IEnumerable<TItem> items, SearchDescriptor searchDescriptor);
}