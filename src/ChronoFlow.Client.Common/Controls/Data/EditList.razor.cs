using ChronoFlow.Shared.Common.Cloning;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Controls.Data;

public partial class EditList<TItem> : ListComponentBase<TItem>
    where TItem : class
{
    private bool _isNew;
    private TItem? _selectedItem;
    private TItem? _selectedItemClone;

    [Parameter]
    public RenderFragment<TItem>? Inline { get; set; }

    [Parameter]
    public EventCallback<TItem?> OnSelect { get; set; }

    [Parameter]
    public EventCallback<TItem> OnSave { get; set; }

    [Parameter]
    public EventCallback<TItem> OnDelete { get; set; }

    [Parameter]
    public EventCallback<List<TItem>> ItemsChanged { get; set; }

    [Parameter, EditorRequired]
    public required Func<TItem> ItemFactory { get; set; }

    private Task SelectAsync(TItem item)
    {
        _isNew = false;

        if (_selectedItem == item)
        {
            _selectedItem = null;
            _selectedItemClone = null;
        }
        else
        {
            _selectedItem = item;
            _selectedItemClone = _selectedItem.CloneInstance();
        }

        return OnSelect.InvokeAsync(_selectedItem);
    }

    private async Task SaveAsync()
    {
        await ItemsChanged.InvokeAsync(Items);
        await OnSave.InvokeAsync(_selectedItem);

        _isNew = false;
        _selectedItem = null;
        _selectedItemClone = null;
    }

    private async Task DeleteAsync(TItem item)
    {
        if (item != null)
            Items.Remove(item);

        _isNew = false;
        _selectedItem = null;
        _selectedItemClone = null;

        await ItemsChanged.InvokeAsync(Items);
        await OnSave.InvokeAsync(item);
    }

    private void Cancel()
    {
        if (!_isNew && _selectedItem != null && _selectedItemClone != null)
            ReplaceWithClone(_selectedItem);
        else if (_isNew && _selectedItem != null)
            Items.Remove(_selectedItem);

        _isNew = false;
        _selectedItem = null;
        _selectedItemClone = null;
    }

    private void AddNew()
    {
        _isNew = true;
        _selectedItem = ItemFactory.Invoke();
        _selectedItemClone = _selectedItem.CloneInstance();

        Items.Add(_selectedItem);
    }

    private string GetItemClasses(bool isSelected)
    {
        var selected = isSelected ? "selected" : null;
        return $"c-edit-list-item {selected}";
    }

    private void ReplaceWithClone(TItem item)
    {
        if (_selectedItemClone == null || !Items.Contains(item))
            return;

        var selectedItemIndex = Items.IndexOf(item);

        Items.Insert(selectedItemIndex, _selectedItemClone);
        Items.Remove(item);
    }
}
