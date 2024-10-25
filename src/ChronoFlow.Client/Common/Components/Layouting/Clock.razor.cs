using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ChronoFlow.Client.Common.Components.Layouting;

public partial class Clock : ComponentBase, IDisposable
{
    private Timer? _internalTimer;

    public void Dispose()
    {
        if (_internalTimer == null)
            return;

        _internalTimer.Stop();
        _internalTimer.Dispose();
    }

    protected override void OnInitialized()
    {
        var oneSecondInterval = TimeSpan.FromSeconds(1);
        _internalTimer = new Timer(oneSecondInterval.TotalMilliseconds)
        {
            AutoReset = true,
        };

        _internalTimer.Elapsed += OnTick;
        _internalTimer.Start();
    }

    private void OnTick(object? sender, ElapsedEventArgs args)
    {
        _ = InvokeAsync(StateHasChanged);
    }
}
