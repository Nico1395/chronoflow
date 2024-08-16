using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ChronoFlow.Server.Common.Persistence.Context;

public sealed class DbContextModelOptions<TDbContext>
    where TDbContext : DbContext
{
    internal DbContextModelOptions() { }

    private Assembly[]? _entityConfigurationAssemblies;

    public DbContextModelOptions<TDbContext> ScanForEntityConfigurationsInAssemblies(params Assembly[] entityConfigurationAssemblies)
    {
        _entityConfigurationAssemblies = entityConfigurationAssemblies;
        return this;
    }

    internal Assembly[] GetEntityConfigurationAssemblies() => _entityConfigurationAssemblies ?? [];
}
