using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.Layouts;

public partial class Logo : ComponentBase
{
    [Parameter]
    public string? Url { get; set; }

    private string GetClasses()
    {
        var hasUrl = Url != null ? "has-url" : null;
        return $"l-logo {hasUrl}";
    }
}
