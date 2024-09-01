using System.Reflection;

namespace ChronoFlow.Server.Common.Controllers;

public sealed class ControllerOptions
{
    internal ControllerOptions() { }

    private readonly List<Assembly> _assemblies = [];

    public IReadOnlyList<Assembly> Assemblies => _assemblies;

    internal void AddAssembliesInternal(params Assembly[] assemblies)
    {
        _assemblies.AddRange(assemblies);
    }
}
