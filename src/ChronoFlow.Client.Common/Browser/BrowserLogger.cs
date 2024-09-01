using Microsoft.JSInterop;

namespace ChronoFlow.Client.Common.Browser;

internal sealed class BrowserLogger(IJSRuntime _jsRuntime) : IBrowserLogger
{
    public ValueTask LogAsync(string message)
    {
        return _jsRuntime.InvokeVoidAsync("console.log", message);
    }

    public ValueTask LogAsync(Exception ex)
    {
        return LogAsync($"{ex.Message}: {ex}");
    }
}
