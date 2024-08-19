using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.Processing.Search;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Data;

public partial class ContainerList<TItem> : ComponentBase
    where TItem : class
{
    private bool _canSort;
    private string? _searchTerm;
    private ContainerListSortOption<TItem>? _selectedSortOption;

    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private ILocalSearchEngine LocalSearchEngine { get; set; } = null!;

    [Parameter, EditorRequired]
    public required List<TItem> Items { get; set; }

    [Parameter]
    public List<ContainerListSortOption<TItem>> SortOptions { get; set; } = [];

    [Parameter]
    public bool Sortable { get; set; } = true;

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter]
    public RenderFragment<TItem>? Item { get; set; }

    [Parameter]
    public Type? Template { get; set; }

    [Parameter]
    public Func<TItem, Dictionary<string, object?>>? TemplateParameterFactory { get; set; }

    protected override void OnInitialized()
    {
        _canSort = Sortable && SortOptions.Count > 0;

        if (Template != null && TemplateParameterFactory == null)
            throw new InvalidOperationException($"If the {nameof(Template)} parameter is provided, the {nameof(TemplateParameterFactory)} also has to be provided.");

        if (Template != null && !Template.IsAssignableTo(typeof(ContainerListItemTemplate<TItem>)))
            throw new InvalidOperationException($"The type of template has to implement the abstract class {typeof(ContainerListItemTemplate<TItem>)}");
    }

    private List<TItem> GetProcessedItems()
    {
        var searchedItems = SearchItems(Items);
        var sortedItems = SortItems(searchedItems);

        return sortedItems.ToList();
    }

    private IEnumerable<TItem> SearchItems(IEnumerable<TItem> items)
    {
        var searchDescriptor = new SearchDescriptor() { SearchTerm = _searchTerm, };
        return LocalSearchEngine.SearchItems(items, searchDescriptor);
    }

    private IEnumerable<TItem> SortItems(IEnumerable<TItem> items)
    {
        if (_selectedSortOption == null)
            return items;

        if (_selectedSortOption.Direction == ContainerListSortDirection.Ascending)
            return items.OrderBy(_selectedSortOption.Field);
        else
            return items.OrderByDescending(_selectedSortOption.Field);
    }
}
