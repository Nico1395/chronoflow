using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.Processing.Search;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChronoFlow.Client.Common.Controls.Data;

public abstract class ListComponentBase<TItem> : ComponentBase
    where TItem : class
{
    protected bool _canSort;
    protected string? _searchTerm;
    protected ListSortOption<TItem>? _selectedSortOption;

    [Inject]
    protected ILocalizer Localizer { get; set; } = null!;

    [Inject]
    protected ILocalSearchEngine LocalSearchEngine { get; set; } = null!;

    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter]
    public List<TItem> Items { get; set; } = [];

    [Parameter]
    public Func<List<TItem>>? ItemSource { get; set; }

    [Parameter]
    public List<ListSortOption<TItem>> SortOptions { get; set; } = [];

    [Parameter]
    public bool Sortable { get; set; } = true;

    [Parameter]
    public bool Searchable { get; set; } = true;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter]
    public RenderFragment? NoItems { get; set; }

    [Parameter]
    public RenderFragment<TItem>? Item { get; set; }

    [Parameter]
    public Type? Template { get; set; }

    [Parameter]
    public Func<TItem, Dictionary<string, object?>>? TemplateParameterFactory { get; set; }

    [Parameter]
    public bool Pageable { get; set; } = true;

    [Parameter]
    public int PageCount { get; set; } = 20;

    [Parameter]
    public int CurrentPage { get; set; } = 1;

    [Parameter]
    public EventCallback<int> CurrentPageChanged { get; set; }

    protected override void OnParametersSet()
    {
        _canSort = Sortable && SortOptions.Count > 0;

        if (Template != null && TemplateParameterFactory == null)
            throw new InvalidOperationException($"If the {nameof(Template)} parameter is provided, the {nameof(TemplateParameterFactory)} also has to be provided.");

        if (Template != null && !Template.IsAssignableTo(typeof(ListItemComponentBase<TItem>)))
            throw new InvalidOperationException($"The type of template has to implement the abstract class {typeof(ListItemComponentBase<TItem>)}");

        _selectedSortOption = SortOptions.FirstOrDefault(o => o.IsDefault);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (ItemSource != null)
            Items = ItemSource.Invoke();
    }

    protected Dictionary<int, List<TItem>> GetItemGroups()
    {
        var processedItems = GetProcessedItems();
        var itemGroups = new Dictionary<int, List<TItem>>();

        if (!Pageable)
        {
            itemGroups[1] = processedItems;
            return itemGroups;
        }

        var totalItems = processedItems.Count;
        var totalPages = (int)Math.Ceiling((double)totalItems / PageCount);

        for (var i = 0; i < totalPages; i++)
        {
            var pageItems = processedItems.Skip(i * PageCount).Take(PageCount).ToList();
            itemGroups.Add(i + 1, pageItems);
        }

        return itemGroups;
    }

    protected List<TItem> GetSelectedItemGroup(Dictionary<int, List<TItem>> itemGroups)
    {
        return itemGroups.TryGetValue(CurrentPage, out var selectedItemGroup) ? selectedItemGroup : [];
    }

    protected async ValueTask SelectItemGroupAsync(int itemGroupIndex)
    {
        CurrentPage = itemGroupIndex;
        StateHasChanged();

        await CurrentPageChanged.InvokeAsync(CurrentPage);
        await JsRuntime.InvokeVoidAsync("scrollTo", 0, 0);
    }

    private List<TItem> GetProcessedItems()
    {
        var searchedItems = SearchItems(Items);
        var sortedItems = SortItems(searchedItems);

        return sortedItems.ToList();
    }

    private IEnumerable<TItem> SearchItems(IEnumerable<TItem> items)
    {
        if (string.IsNullOrEmpty(_searchTerm))
            return items;

        var searchDescriptor = new SearchDescriptor() { SearchTerm = _searchTerm, };
        return LocalSearchEngine.SearchItems(items, searchDescriptor);
    }

    private IEnumerable<TItem> SortItems(IEnumerable<TItem> items)
    {
        if (_selectedSortOption == null)
            return items;

        if (_selectedSortOption.Direction == ListSortDirection.Ascending)
            return items.OrderBy(_selectedSortOption.Field);
        else
            return items.OrderByDescending(_selectedSortOption.Field);
    }
}
