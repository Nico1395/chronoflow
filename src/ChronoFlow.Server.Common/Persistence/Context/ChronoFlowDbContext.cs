using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.Common.Persistence.Context;

internal sealed class ChronoFlowDbContext(
    DbContextModelOptions<ChronoFlowDbContext> modelOptions,
    DbContextOptions<ChronoFlowDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var assembly in modelOptions.GetEntityConfigurationAssemblies())
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
