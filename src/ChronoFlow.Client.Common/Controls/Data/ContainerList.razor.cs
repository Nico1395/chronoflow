using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.Processing.Search;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter, EditorRequired]
    public required List<TItem> Items { get; set; }

    [Parameter]
    public List<ContainerListSortOption<TItem>> SortOptions { get; set; } = [];

    [Parameter]
    public bool Sortable { get; set; } = true;

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

    protected override void OnInitialized()
    {
        _canSort = Sortable && SortOptions.Count > 0;

        if (Template != null && TemplateParameterFactory == null)
            throw new InvalidOperationException($"If the {nameof(Template)} parameter is provided, the {nameof(TemplateParameterFactory)} also has to be provided.");

        if (Template != null && !Template.IsAssignableTo(typeof(ContainerListItemComponentBase<TItem>)))
            throw new InvalidOperationException($"The type of template has to implement the abstract class {typeof(ContainerListItemComponentBase<TItem>)}");
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

        if (_selectedSortOption.Direction == ContainerListSortDirection.Ascending)
            return items.OrderBy(_selectedSortOption.Field);
        else
            return items.OrderByDescending(_selectedSortOption.Field);
    }

    private Dictionary<int, List<TItem>> GetItemGroups()
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

    private List<TItem> GetSelectedItemGroup(Dictionary<int, List<TItem>> itemGroups)
    {
        return itemGroups.TryGetValue(CurrentPage, out var selectedItemGroup) ? selectedItemGroup : [];
    }

    private async ValueTask SelectItemGroupAsync(int itemGroupIndex)
    {
        CurrentPage = itemGroupIndex;
        StateHasChanged();

        await CurrentPageChanged.InvokeAsync(CurrentPage);
        await JsRuntime.InvokeVoidAsync("scrollTo", 0, 0);
    }
}
