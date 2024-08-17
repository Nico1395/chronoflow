namespace ChronoFlow.Client.Common.Browser;

/// <summary>
/// Service for interacting with the local storage of the browser.
/// </summary>
public interface ILocalStorage
{
    /// <summary>
    /// Method sets an item in the local storage of the browser asynchronously.
    /// </summary>
    /// <param name="key">Key of the item.</param>
    /// <param name="value">Value of the item.</param>
    public ValueTask SetItemAsync(string key, string value);

    /// <summary>
    /// Method gets an item from the local storage of the browser asynchronously.
    /// </summary>
    /// <param name="key">Key of the item to get.</param>
    /// <returns>Value of the item, if found.</returns>
    public ValueTask<string?> GetItemAsync(string key);

    /// <summary>
    /// Method gets all items from the local storage of the browser asynchronously.
    /// </summary>
    /// <returns>Dictionary of all items from the local storage.</returns>
    public ValueTask<Dictionary<string, string>> GetItemsAsync();

    /// <summary>
    /// Method gets the key of the item at the specified index from the local storage of the browser asynchronously.
    /// </summary>
    /// <param name="index">Index of the item, whose key is to be returned.</param>
    /// <returns>Key of the item a the specified index, if found.</returns>
    public ValueTask<string?> Key(int index);

    /// <summary>
    /// Method removes an item with the specified <paramref name="key"/> from the local storage of the browser asynchronously.
    /// </summary>
    /// <param name="key">Key of the item to be removed.</param>
    public ValueTask RemoveItemAsync(string key);

    /// <summary>
    /// Method clears the local storage of the browser asynchronously.
    /// </summary>
    public ValueTask ClearAsync();
}
