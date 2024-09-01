using System.Reflection;

namespace ChronoFlow.Server.Common.Controllers;

public sealed class ControllerOptionsBuilder
{
    private readonly ControllerOptions _options = new();

    public ControllerOptionsBuilder ScanInAssembly(Assembly assembly)
    {
        _options.AddAssembliesInternal(assembly);
        return this;
    }

    public ControllerOptionsBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _options.AddAssembliesInternal(assemblies);
        return this;
    }

    internal ControllerOptions Build()
    {
        return _options;
    }
}
