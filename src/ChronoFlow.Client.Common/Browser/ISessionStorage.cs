namespace ChronoFlow.Client.Common.Browser;

internal interface ISessionStorage
{
    public ValueTask<string?> Key(int index);
    public ValueTask<Dictionary<string, string>> GetItemsAsync();
    public ValueTask<string?> GetItemAsync(string key);
    public ValueTask SetItemAsync(string key, string value);
    public ValueTask RemoveItemAsync(string key);
    public ValueTask Clear();
}
