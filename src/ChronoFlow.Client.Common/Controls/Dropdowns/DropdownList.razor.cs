using ChronoFlow.Client.Common.Controls.Forms;
using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChronoFlow.Client.Common.Controls.Dropdowns;

public partial class DropdownList<TItem> : FormControlComponentBase, IAsyncDisposable
{
    private IJSObjectReference? _floatingContainerReference;
    private DotNetObjectReference<DropdownList<TItem>>? _dropdownListReference;
    private string? _floatingContainerId;

    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter]
    public List<TItem> Items { get; set; } = [];

    [Parameter]
    public TItem? SelectedItem { get; set; }

    [Parameter]
    public EventCallback<TItem?> SelectedItemChanged { get; set; }

    [Parameter]
    public bool Opened { get; set; }

    [Parameter]
    public EventCallback<bool> OpenedChanged { get; set; }

    [Parameter]
    public string? Tooltip { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public bool OnlyShowPlaceholder { get; set; }

    [Parameter]
    public string? IconLeft { get; set; }

    [Parameter]
    public string? IconLeftSize { get; set; }

    [Parameter]
    public string? IconRight { get; set; }

    [Parameter]
    public string? IconRightSize { get; set; }

    [Parameter]
    public Func<TItem, string>? DisplayValue { get; set; }

    [Parameter]
    public Func<TItem, string>? ItemIconLeft { get; set; }

    [Parameter]
    public Func<TItem, string>? ItemIconRight { get; set; }

    [Parameter]
    public bool Localize { get; set; }

    [JSInvokable(nameof(OnFocusLostAsync))]
    public Task OnFocusLostAsync()
    {
        return ToggleAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_floatingContainerReference != null)
            await _floatingContainerReference.InvokeVoidAsync("removeClickOfFocusEventListener");

        _dropdownListReference?.Dispose();
    }

    protected override void OnParametersSet()
    {
        Id ??= Guid.NewGuid().ToString();
        _floatingContainerId = $"{Id}-items";
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _floatingContainerReference = await JsRuntime.InvokeAsync<IJSObjectReference>(
                "createFloatingContainer",
                _dropdownListReference = DotNetObjectReference.Create(this),
                _floatingContainerId);
        }
    }

    private string GetClasses()
    {
        var invalid = !IsValid ? "invalid" : null;
        var opened = Opened ? "opened" : null;
        var disabled = GetDisabled() ? "disabled" : null;

        return $"c-dropdown-list {invalid} {opened} {disabled}".Trim();
    }

    private string GetStyles()
    {
        var width = Width != null ? $"width:{Width};" : null;
        return $"{width}";
    }

    private string? GetDisplayValue(TItem item)
    {
        var displayValue = DisplayValue != null ? DisplayValue(item) : item?.ToString();
        if (displayValue == null)
            return null;

        if (Localize)
            displayValue = Localizer[displayValue];

        return displayValue;
    }

    private async Task ToggleAsync()
    {
        await OpenedChanged.InvokeAsync(Opened = !Opened);
        await Task.Run(StateHasChanged);

        if (_floatingContainerReference != null)
        {
            if (Opened)
                await _floatingContainerReference.InvokeVoidAsync("onOpen");
            else
                await _floatingContainerReference.InvokeVoidAsync("onClose");
        }
    }

    private async Task OnSelectItemAsync(TItem item)
    {
        if (SelectedItem != null && SelectedItem.Equals(item))
            SelectedItem = default;
        else
            SelectedItem = item;

        await SelectedItemChanged.InvokeAsync(SelectedItem);
        await ToggleAsync();
    }
}
