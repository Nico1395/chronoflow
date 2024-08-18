using Microsoft.JSInterop;

namespace ChronoFlow.Client.Common.Browser;

internal sealed class SessionStorage(IJSRuntime _jsRuntime) : ISessionStorage
{
    public ValueTask<string?> Key(int index)
    {
        return _jsRuntime.InvokeAsync<string?>("sessionStorage.key", index);
    }

    public ValueTask<Dictionary<string, string>> GetItemsAsync()
    {
        return _jsRuntime.InvokeAsync<Dictionary<string, string>>("sessionStorageManager.getItems");
    }

    public ValueTask<string?> GetItemAsync(string key)
    {
        return _jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", key);
    }

    public ValueTask SetItemAsync(string key, string value)
    {
        return _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, value);
    }

    public ValueTask RemoveItemAsync(string key)
    {
        return _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
    }

    public ValueTask Clear()
    {
        return _jsRuntime.InvokeVoidAsync("sessionStorage.clear");
    }
}
